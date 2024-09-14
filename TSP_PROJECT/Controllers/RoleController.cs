using TSP_PROJECT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TSP_PROJECT.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) { 
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRole()
        {
            ViewBag.rolelist = _roleManager.Roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string rname)
        {
            ViewBag.rolelist = _roleManager.Roles;
            if (string.IsNullOrEmpty(rname))
            {
                ViewBag.msg = "Role cannot be Null or Empty";
                return View();
            }

            IdentityRole r = new IdentityRole(rname);
            if(await _roleManager.RoleExistsAsync(rname))
            {
                ViewBag.msg = "Role already exist";
                return View();
            }
            else
            {
                var result = await _roleManager.CreateAsync(r);
                if(result.Succeeded)
                {
                    ViewBag.msg = rname + " Role has been created Successfully";
                    return View();
                }
                else
                {
                    ViewBag.msg = "Sorry ! Could not create the Role";
                    return View();
                }
            }
        }


        public IActionResult AssignRole()
        {
            ViewBag.userlist = _userManager.Users;
            ViewBag.rolelist = _roleManager.Roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string useremail, string userrole)
        {
            ViewBag.userlist = _userManager.Users;
            ViewBag.rolelist = _roleManager.Roles;

            if (string.IsNullOrEmpty(useremail))
            {
                ViewBag.msg = "User Email cannot be Null or Empty";
                return View();
            }

            if (string.IsNullOrEmpty(userrole))
            {
                ViewBag.msg = "User Role cannot be Null or Empty";
                return View();
            }

            IdentityUser? user = await _userManager.FindByEmailAsync(useremail);

            if (user == null)
            {
                ViewBag.msg = "Sorry ! Could not find this User.";
                return View();
            }

            if(await _userManager.IsInRoleAsync(user, userrole))
            {
                ViewBag.msg = "This Role is alredy exist for this User.";
                return View();
            }

            IdentityResult result= await _userManager.AddToRoleAsync(user, userrole);
            if (result.Succeeded)
            {
                ViewBag.msg = "Role has been assigned to the User Successfully.";
                return View();
            }
            else
            {
                ViewBag.msg = "Sorry ! Could not assign Role to the User.";
                return View();
            }
        }
    }
}
