using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.Examples.Repository.FluentNHibernate.Location
{
	public class City : IdentityObject, ICity
	{
		public static string DefaultLanguageCode { get; set; } = "en";

		public virtual IList<ICityName> CityNames { get; set; } = new List<ICityName>();

		public virtual string LocalizableName
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