using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenXmlApplication.Interfaces;
using OpenXmlApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenXmlApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWord _word;

        public WordController(IWord word)
        {
            _word = word;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ReplaceDocument()
        {
            var curDir = Environment.CurrentDirectory;
            var dataList = new List<ReplaceDocumentDto>
            {
                new ReplaceDocumentDto { BookmarkName = "text", BookmarkType = Utils.BookmarkType.Text, Text = "这是文本这是文本这是文本这是文本这是文本这是文本这是文本这是文本这是文本这是文本这是文本这是文本这是文本" },
                new ReplaceDocumentDto { BookmarkName = "table", BookmarkType = Utils.BookmarkType.Table, TableData = new List<List<string>>{ new List<string> { "文本1", "文本2", "文本3", "文本4" }, new List<string> { "1", "2", "3", "4" } } },
                new ReplaceDocumentDto { BookmarkName = "picture", BookmarkType = Utils.BookmarkType.Picture, Picture = curDir + "/Test.png" }
            };

            _word.SaveWord(dataList);

            return Ok();
        }
    }
}
