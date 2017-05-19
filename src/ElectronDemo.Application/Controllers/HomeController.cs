using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Kastwey.EpubReader;

namespace ElectronDemo.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
