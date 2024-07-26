using Autodesk.Connectivity.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class FileAssocDTO
	{
		private long _fileId;
		public long FileId { get { return _fileId; } set { _fileId = value; } }
		private string _fileName;
		public string FileName { get { return _fileName; } set { _fileName = value; } }
		public FileAssocDTO(ItemFileAssoc itemFileAssoc) {
			this._fileName = itemFileAssoc.FileName;
			this.FileId = itemFileAssoc.CldFileId;
		}
	}
}
