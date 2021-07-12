using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository.EntityFramework;

namespace Simplify.Examples.Repository.EntityFramework.Location
{
	public class City : IdentityObject, ICity
	{
		public static string DefaultLanguageCode { get; set; } = "en";

		public IList<ICityName> CityNames { get; set; } = new List<ICityName>();

		public string LocalizableName
		{
			get
			{
				if (CityNames.Count <= 0) return "";

				var itemName = CityNames.FirstOrDefault(p => p.Language == Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

				if (itemName != null)
					return itemName.Name;

				return CityNames.FirstOrDefault(x => x.Language == DefaultLanguageCode)?.Name ?? "";
			}
		}
	}
}