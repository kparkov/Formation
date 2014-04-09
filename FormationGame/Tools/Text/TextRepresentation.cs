﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;

namespace FormationGame.Tools.Text
{
	public class TextRepresentation
	{
		private bool _rootRendered = false;

		public MvcHtmlString DisplayObject(object obj)
		{
			return MvcHtmlString.Create("<div style='font-family: monospace; font-size: 18px;'><h2>Objects added to view</h2>" + ParseRootObject(obj) + "</div>");
		}

		protected string ParseEnumerable(IEnumerable list)
		{
			var listItems = new List<string>();

			var prefix = _rootRendered ? "<em><strong style='padding-right:1em;'>[property is a list currently containing these objects:]</strong></em>" : "";

			foreach (var obj in list)
			{
				listItems.Add(ParseRootObject(obj));
			}

			return prefix + String.Join("", listItems);
		}

		protected string ParseRootObject(object obj)
		{
			if (obj == null)
			{
				return "<em>[null]</em>";
			}

			if (obj is IEnumerable && !(obj is string))
			{
				return ParseEnumerable((IEnumerable)obj);
			}

			if (IsPrimitiveType(obj.GetType()))
			{
				return "<span style='padding-right:1em;'>" + obj.ToString() + "</span>";
			}

			_rootRendered = true;

			var objectName = "<div style='margin-left: 2em;'><h3 style='text-decoration: underline; color: #50a5a0;'>" + obj.GetType().Name + "</h3>";

			var list = new List<string>();

			foreach (var propertyInfo in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				var propertyName = propertyInfo.Name;
				var value = ParseRootObject(propertyInfo.GetGetMethod().Invoke(obj, new object[] {}));

				var formatted = String.Format("<li style='list-style-type: none;'><strong style='color:blue; font-family:sans-serif;'>{0}</strong>: <strong>{1}</strong></li>", propertyName, value);

				list.Add(formatted);
			}

			return objectName + "<ul style='padding:0;'>" + String.Join("", list) + "</ul></div>";
		}

		protected bool IsPrimitiveType(Type t)
		{
			var primitives = new List<Type>()
			{
				typeof(Decimal), typeof(String), typeof(Int32), typeof(Int16), typeof(Int64), typeof(Boolean),
				typeof(Nullable), typeof(DateTime)
			};

			return t.IsPrimitive || primitives.Contains(t);
		}
	}
}