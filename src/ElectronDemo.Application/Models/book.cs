using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronDemo.Application.Models
{
	public class book
	{
		public string Title { get; set; }

		public string Author { get; set; }

		public List<Chapter> Chapters { get; set; }

		public string FileName { get; set; }

	}
}
