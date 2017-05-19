using ElectronDemo.Application.Models;
using Kastwey.EpubReader;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElectronDemo.Application.Controllers
{
	public class EbookReaderController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}


		public async Task<IActionResult> Read(string path)
		{
			/*string s = Path.DirectorySeparatorChar.ToString();
			string filePath = $"d:{s}libros{s}Winslow Don - El Cartel.epub";
			*/
			if (!System.IO.File.Exists(path))
			{
				return NotFound("The book path was not found.");
			}
			var book = EpubReader.ReadBook(path);
			// return book.Chapters.Skip(3).First().HtmlContent; ;
			List<Models.Chapter> chapters = new List<Models.Chapter>();
			Func<EpubChapter, Chapter> funcConvert = null;
			funcConvert = new Func<EpubChapter, Chapter>((epChapter) =>
			{
				Chapter chapter = new Chapter { FileName = epChapter.ContentFileName, Title = epChapter.Title };
				if (epChapter.SubChapters != null && epChapter.SubChapters.Any())
				{
					foreach (var epSubChapter in epChapter.SubChapters)
					{
						chapter.Chapters.Add(funcConvert(epSubChapter));
					}
				}
				return chapter;
			});
			foreach (var epChapter in book.Chapters)
			{
				chapters.Add(funcConvert(epChapter));
			}
			// return View();
			var bookModel = new Models.book
			{
				Title = book.Title,
				Author = book.Author,
				Chapters = chapters,
				FileName = path
			};
			return View(bookModel);
		}
	}
}
