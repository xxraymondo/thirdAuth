using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thirdAuth.Data;
using thirdAuth.Models;
using thirdAuth.ViewModel;


namespace thirdAuth.Controllers
{
    public class MangeAccountController : Controller
    {
       private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _uManger;
        private readonly SignInManager<IdentityUser> _uSignInManger;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public MangeAccountController(ApplicationDbContext dbContext, UserManager<IdentityUser> uManger,
            SignInManager<IdentityUser> uSignInManger, RoleManager<IdentityRole> RoleManager)
        {
            _RoleManager = RoleManager;
            _uSignInManger = uSignInManger;
            _uManger = uManger;
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Logout( )
        {
            await _uSignInManger.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public  IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult StudentLogin()
        {

          

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentLogin(StudentViewModel us)
        {
            

            var st = await _dbContext.students.SingleOrDefaultAsync( q=> q.AppUser.Email==us.Email);

            if (st != null)
            {
                IdentityUser iduser = new IdentityUser() { Email = st.AppUser.Email, PasswordHash = st.AppUser.PasswordHash };
                await _uSignInManger.SignInAsync(iduser,true);
                return Content("true");
            }

            return Content("false");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> addRole()
        {
            IdentityRole StudentRole = new IdentityRole() { Name = "StudentRole" };
            IdentityRole TeacherRole = new IdentityRole() { Name = "TeacherRole" };
            var result1 = await _RoleManager.CreateAsync(StudentRole);
            var result2 = await _RoleManager.CreateAsync(TeacherRole);
            
            if (result1.Succeeded&&result2.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }
            return Content("error ");
        }
        [HttpGet]
        public IActionResult RegisterNewAcoount()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RegisterNewStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewStudent(StudentViewModel viewModel)
         {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser() { Email = viewModel.Email, UserName = viewModel.Email };
                var result =await _uManger.CreateAsync(newUser, viewModel.Password);//
                if (result.Succeeded)
                {
                    Student s = new Student(){year = viewModel.year,AppUserId = newUser.Id};
                    _dbContext.Add(s);
                    _dbContext.SaveChanges();
                    await _uManger.AddToRoleAsync(newUser, "StudentRole");
                    await _uSignInManger.SignInAsync(newUser, true);//
                    
                    return RedirectToAction("Index", "Home");

                }
            }     
                return View(viewModel);  
        }
        [HttpGet]
        public IActionResult RegisterNewTeacher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewTeacher(TeacherViewModel viewModel)
        {
            var newUser = new IdentityUser() { Email = viewModel.Email, UserName = viewModel.Email };
            var result = await _uManger.CreateAsync(newUser, viewModel.Password);
            if (result.Succeeded)
            {
                Teacher t = new Teacher()
                {
                    Salary = viewModel.Salary,
                    appUserId = newUser.Id

                };
                _dbContext.Add(t);
                _dbContext.SaveChanges();
                await _uManger.AddToRoleAsync(newUser, "TeacherRole");
                await _uSignInManger.SignInAsync(newUser, true);
                return RedirectToAction( "RegisterNewAcoount");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }


    }
}
