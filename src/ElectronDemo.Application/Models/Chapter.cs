using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronDemo.Application.Models
{
    public class Chapter
    {
		public string Title { get; set; }

		public string FileName { get; set; }

		public List<Chapter> Chapters { get; set; } = new List<Chapter>();

	}
}
