using OpenXmlApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenXmlApplication.Interfaces
{
    public interface IWord
    {
        public void SaveWord(IList<ReplaceDocumentDto> input);
    }
}
