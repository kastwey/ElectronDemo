using Kastwey.EpubReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronDemo.Application.Extensions
{
	public static class BookExtensions
	{
		public static List<EpubChapter> GetAllChapters(this EpubBook book)
		{
			List<EpubChapter> chapters = new List<EpubChapter>();
			Action<EpubChapter> ActAddSubChapters = null;
			ActAddSubChapters = new Action<EpubChapter>((epChapter) =>
			{
				chapters.Add(epChapter);
				if (epChapter.SubChapters != null && epChapter.SubChapters.Any())
				{
					foreach (var epSubChapter in epChapter.SubChapters)
					{
						ActAddSubChapters(epSubChapter);
					}
				}
			});
			foreach (var epChapter in book.Chapters)
			{
				ActAddSubChapters(epChapter);
			}
			return chapters;
		}

	}
}
