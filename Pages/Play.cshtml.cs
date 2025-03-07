using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Waveylength.Pages
{
	public class PlayModel : PageModel
	{
		// This property will receive the 'code' parameter from the URL
		[BindProperty(SupportsGet = true)]
		public string Code { get; set; }

		public void OnGet()
		{
			// You can add additional logic here if needed
		}
	}
}
