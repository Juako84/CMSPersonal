using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSPersonal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSPersonal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public AdminController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMrg, SignInManager<AppUser> signMgr)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            signInManager = signMgr;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}