﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormationGame.Controllers
{
	//Idas klasser
    public class ClericController : FormationBaseController
    {
        public ActionResult IdaView()
        {
            return View();
        }

		// KODE TIL LEKTION 3
	    public ActionResult Index()
	    {
			// Her begynder vi ved tredje lektion:


			// Denne metode kan benyttes til at vise indholdet af variabler
		    return ShowObjects(/*Slet denne kommentar og erstat med variabler*/);
	    }
    }

	// MODEL:
	public class Cleric
	{
	}
}
