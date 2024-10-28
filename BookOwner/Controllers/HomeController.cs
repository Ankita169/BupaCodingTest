using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookOwner.Models;

namespace BookOwner.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookOwnerService _bookOwnerService;

        public HomeController()
        {
            _bookOwnerService = new BookOwnerService();
        }
        // GET: BookOwner
        public async Task<ActionResult> Book()
        {

            var bookOwners = await _bookOwnerService.GetBooksByCategory();
            if (bookOwners == null||!bookOwners.Any())
            {
                return View("Error");

            }
            else
            {
                // Categorize  by age and order by book
                var adults = bookOwners.Where(adultBook => adultBook.Age >= 18 ).Select(adultsort=> { adultsort.Books = adultsort.Books.OrderBy(adulto => adulto.Name).ToList(); return adultsort; }).ToList();
                var children = bookOwners.Where(childrenBook => childrenBook.Age < 18).Select(adultsort => { adultsort.Books = adultsort.Books.OrderBy(adulto => adulto.Name).ToList(); return adultsort; }).ToList();
                // Pass data to the view
                var viewModel = new BookOwnerViewModel
                {
                    Adults = adults,
                    Children = children
                };
                return View(viewModel);
            }

        }
        // Get : HardCoverOnly     
        public async Task<ActionResult> HardCoverBooks()
        {
            var hardCoverBooks = await _bookOwnerService.GetBooksByCategory();
            if (hardCoverBooks == null || !hardCoverBooks.Any())
            {
                return View("HardCoverBooks");
            }
            else
            {

                var HardCover = hardCoverBooks.Where(owner => owner != null)
                .SelectMany(ownercover => ownercover.Books).Where(type => type.Type == "Hardcover").OrderBy(cover => cover.Name).ToList();
                return View("HardCoverBooks", HardCover);
            }
        }
        //Get All Books  owned by adult and child 
        public async Task<ActionResult> AllBooks()
        {
            var AllBooks = await _bookOwnerService.GetBooksByCategory();
            if (AllBooks == null || !AllBooks.Any())
            {
                return View("AllBooks");
            }
            else
            {
                var Allbook = AllBooks
                           .Where(owner => owner.Books != null)
                           .SelectMany(listbooks => listbooks.Books)
                           .OrderBy(bookssort => bookssort.Name).ToList();

                return View("AllBooks", Allbook);
            }

        }

    }

}
