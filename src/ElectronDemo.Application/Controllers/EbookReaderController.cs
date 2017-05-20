using ElectronDemo.Application.Extensions;
using ElectronDemo.Application.Models;
using Kastwey.EpubReader;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronDemo.Application.Controllers
{
	public class EbookReaderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Read(string path)
		{
			if (!System.IO.File.Exists(path))
			{
				return NotFound("The book path was not found.");
			}
			var book = EpubReader.ReadBook(path);
			var bookModel = new Models.book
			{
				Title = book.Title,
				Author = book.Author,
				FileName = path
			};
			List<Models.Chapter> chapters = new List<Models.Chapter>();
			Func<EpubChapter, Chapter> funcConvert = null;
			funcConvert = new Func<EpubChapter, Chapter>((epChapter) =>
			{
				Chapter chapter = new Chapter { FileName = epChapter.ContentFileName, Title = epChapter.Title, Book = bookModel };
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
			bookModel.Chapters = chapters;
			return View(bookModel);
		}

		[Produces("text/html")]
		[Route("Chapter/{path}/{chapter}")]
		public string ReadChapter(string path, string chapter)
		{
			if (!System.IO.File.Exists(path))
			{
				return "<p>The book path was not found.</p>";
			}
			var book = EpubReader.ReadBook(path);
			var bookChapter = book.GetAllChapters().SingleOrDefault(c => c.ContentFileName == chapter);
			if (bookChapter == null)
			{
				return $"<p>chapter with filename {chapter} was not found.</p>";
			}
			return bookChapter.HtmlContent + Environment.NewLine +
				"<p><a href=\"" + Url.Action("Read", "EbookReader", new { path = path }) + "\">Volver al índice.</a></p>";
		}

		public IActionResult Export(string bookPath, string exportPath)
		{
			if (!System.IO.File.Exists(bookPath))
			{
				return NotFound("The book path was not found.");
			}
			var book = EpubReader.ReadBook(bookPath);
			var regex = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
			List<EpubChapter> allChapters = book.GetAllChapters();
			string wholeBook = String.Join(Environment.NewLine, allChapters.Select(ch => {
				return regex.Replace(ch.HtmlContent, String.Empty);
			}).ToArray());
			try
			{
				System.IO.File.WriteAllText(exportPath, wholeBook, Encoding.UTF8);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

	}
}