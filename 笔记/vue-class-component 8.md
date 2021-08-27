# 前言
> 由于`vue-class-component v8`官方目前还没有成型的文档，本篇简单记录了`v8`与之前版本的差异与新的使用方式。
> 参考地址： https://github.com/vuejs/vue-class-component/issues?q=is%3Aopen+is%3Aissue+label%3Av8+

# 变动
### 装饰器 `@Component` 和 `Vue` 基类的修改
- `@Component` 改名为 `@Option` 
- 如果没有声明任何option，`@Option` 是可选的
- `Vue` 的构造由 `vue-class-component` 包提供
- `Component.registerHooks` 移动到 `Vue.registerHooks`

例如：
```ts
<template>
  <div>{{ count }}</div>
  <button @click="increment">+1</button>
</template>

<script>
import { Vue, Options } from 'vue-class-component'

// Component definition
@Options({
  // Define component options
  watch: {
    count: value => {
      console.log(value)
    }
  }
})
export default class Counter extends Vue {
  // The behavior in class is the same as the current
  count = 0

  increment() {
    this.count++
  }
}
</script>
```
```ts
// Adding lifecycle hooks
import { Vue } from 'vue-class-component'

Vue.registerHooks([
  'beforeRouteEnter',
  'beforeRouteLeave',
  'beforeRouteUpdate'
])
```
### 组合函数的支持和 `this` 值的更改
- 组合函数可以通过 `setup` helper 的包装在类属性初始化器 (class property initializer) 中使用
    - 类属性初始化器 (`class property initializer`) 由底层的 `setup` 函数处理
- 在类属性初始化器中， `this` 的内置属性只有 `$props (及其派生的prop值)`, `$attrs`, `$slots` 和 `$emit` 可用

例如：
```ts
<template>
  <div>Count: {{ counter.count }}</div>
  <button @click="counter.increment()">+</button>
</template>

<script lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { Vue, setup } from 'vue-class-component'

function useCounter () {
  const count = ref(0)

  function increment () {
    count.value++
  }

  onMounted(() => {
    console.log('onMounted')
  })

  return {
    count,
    increment
  }
}

export default class Counter extends Vue {
  counter = setup(() => useCounter())
}
</script>
```
```ts
function useCounter(props, emit) {
  function increment() {
    emit('input', props.count + 1)
  }

  return {
    increment
  }
}

export default class App extends Vue {
  counter = setup(() => {
    return useCounter(this.$props, this.$emit)
  })
}
```
或使用 `super` 类和 `mixins`
```ts
import { ref } from 'vue'
import { setup } from 'vue-class-component'

const Super = setup((props, ctx) => {
  const count = ref(0)

  function increment() {
    count.value++
  }

  return {
    count,
    increment
  }
})

export default class App extends Super {}
```
### class 风格的 `props` 定义
- 使用 `prop` helper 定义 prop 选项
```ts
import { Vue, prop } from 'vue-class-component'

// Define props in a class
class Props {
  count = prop({
    // Same as Vue core's prop option
    type: Number,
    required: true,
    validator: (value) => value >= 0
  })
}

// Pass the Props class to `Vue.with` so that the props are defined in the component
export default class MyComp extends Vue.with(Props) {}
```
- 在 `TypeScript` 中，当只需要定义类型时，可以省略 `prop` helper (这种情况下运行时不会验证)
```ts
import { Vue, prop } from 'vue-class-component'

class Props {
  // optional prop
  foo?: string

  // required prop
  bar!: string

  // optional prop with default
  baz = prop<string>({ default: 'default value' })
}

export default class MyComp extends Vue.with(Props) {}
```
此外，还需要为TypeScript编译器指定 `"useDefineForClassFields": true` 使Vue类组件无需初始化即可知道属性
```ts
{
  "compilerOptions": {
    "useDefineForClassFields": true
  }
}
```
# 与vue 2 + vue-property-decorator 差异
```ts
// example working actually in Vue 2 + vue-property-decorator
@Component
export default class StepAbstract extends Vue {
  @Prop()
  nextStepPath: string;

  public GoToNext(): void {
    this.$router.push(this.nextStepPath);
  }
}

@Component
export default class StepOne extends StepAbstract {
  @Prop()
  customer: ICustomer;

  get fullname(): string {
    return customer.firstname + ' ' + customer.lastname;
  }

@Component
export default class StepTwo extends StepAbstract {
 @Prop()
 payment: IPayment;

 get preferedPaymentMethod(): string {
  // code removed for brievety
 }
}
```
->
```ts
class Props {
  nextStepPath: string
}

export default class StepAbstract extends Vue.props(Props) {
  public GoToNext(): void {
    this.$router.push(this.nextStepPath);
  }
}

// ---

class Props {
  customer: ICustomer
}

export default class StepOne extends StepAbstract.props(Props) {
  get fullname(): string {
    return customer.firstname + ' ' + customer.lastname;
  }

// ---

class Props {
  payment: IPayment
}

export default class StepTwo extends StepAbstract.props(Props) {
 get preferedPaymentMethod(): string {
  // code removed for brievety
 }
}
```
---
```ts
// example working actually in Vue 2 + vue-property-decorator
IValidable interface {
  IsValid (): boolean;
}

@Component
export default class PaymentForm {
  @Ref ()
  refIbanElement: IValidable;
}

<template>
<form>
  <iban-element-from-third-party-component ref = "refIbanElement" />
  <input type = "submit" disabled = "!refIbanElement.IsValid ()" />
</form>
</template>
```
->
```ts
export default class PaymentForm {
  refIbanElement: IValidable;
}

<template>
<form>
  <iban-element-from-third-party-component ref = "refIbanElement" />
  <input type = "submit" disabled = "!refIbanElement.IsValid ()" />
</form>
</template>
```