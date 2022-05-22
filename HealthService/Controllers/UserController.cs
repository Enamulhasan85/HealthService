using HealthService.cs;
using HealthService.CustomAuthentication;
using HealthService.DataAccess;
using HealthService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HealthService.Controllers
{
    [CustomAuthorize(Roles = "systemadmin, admin")]
    public class UserController : Controller
    {
        private HealthServiceContext db = new HealthServiceContext();

        // GET: User  
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Upazilla);
            return View(users.ToList());
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var vm = new RegistrationView()
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password,
                ActivationCode = Guid.NewGuid(),
            };
            vm.IsActive = user.IsActive;
            vm.RoleId = user.Roles.FirstOrDefault().RoleId;

            ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
            {
                Text = r.RoleName,
                Value = r.RoleId.ToString()
            });

            ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name", user.UpazillaId);
            return View(vm);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationView registrationview)
        {
            if (ModelState.IsValid)
            {
                string messageRegistration = string.Empty;
                User user1 = db.Users.Where(r => r.Email == registrationview.Email).FirstOrDefault();
                User user2 = db.Users.Where(r => r.Username == registrationview.Username).FirstOrDefault();
                //string userName = user1.Username;
                if (user1 != null && user1.UserId != registrationview.UserId)
                {
                    ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
                    {
                        Text = r.RoleName,
                        Value = r.RoleId.ToString()
                    });

                    ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");

                    messageRegistration = "Sorry: Email already Exists";
                    ViewBag.Message = messageRegistration;

                    return View(registrationview);
                }
                else if (user2 != null && user2.UserId != registrationview.UserId)
                {
                    ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
                    {
                        Text = r.RoleName,
                        Value = r.RoleId.ToString()
                    });

                    ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");

                    messageRegistration = "Sorry: Username already Exists";
                    ViewBag.Message = messageRegistration;

                    return View(registrationview);
                }
                db.Database.ExecuteSqlCommand("delete FROM [UserRoles] where [UserId] = " + registrationview.UserId);

                var user = db.Users.Find(registrationview.UserId);

                user.UserId = registrationview.UserId;
                user.Username = registrationview.Username;
                user.FirstName = registrationview.FirstName;
                user.LastName = registrationview.LastName;
                user.Email = registrationview.Email;
                user.Password = registrationview.Password;
                user.ActivationCode = registrationview.ActivationCode;
                user.IsActive = registrationview.IsActive;
                user.UpazillaId = registrationview.UpazillaId;
                user.Roles = db.Roles.Where(r => r.RoleId == registrationview.RoleId).ToList();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name", registrationview.UpazillaId);

            ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
            {
                Text = r.RoleName,
                Value = r.RoleId.ToString(),
            });

            return View(registrationview);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
            {
                Text = r.RoleName,
                Value = r.RoleId.ToString()
            });

            ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification  
                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                User user1 = db.Users.Where(r => r.Username == registrationView.Username).FirstOrDefault();
                if (!string.IsNullOrEmpty(userName))
                {
                    ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
                    {
                        Text = r.RoleName,
                        Value = r.RoleId.ToString()
                    });

                    ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");

                    messageRegistration = "Sorry: Email already Exists";
                    ViewBag.Message = messageRegistration;

                    return View(registrationView);
                }
                if (user1!=null)
                {
                    ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
                    {
                        Text = r.RoleName,
                        Value = r.RoleId.ToString()
                    });

                    ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");

                    messageRegistration = "Sorry: Username already Exists";
                    ViewBag.Message = messageRegistration;

                    return View(registrationView);
                }
                //Save User Data   
                using (HealthServiceContext dbContext = new HealthServiceContext())
                {
                    var user = new User()
                    {
                        Username = registrationView.Username,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        Password = registrationView.Password,
                        ActivationCode = Guid.NewGuid(),
                        UpazillaId = registrationView.UpazillaId,
                    };

                    user.IsActive = true;
                    user.Roles = dbContext.Roles.Where(r => r.RoleId == registrationView.RoleId).ToList();
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }

                //Verification Email  
                //VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
                ViewBag.RoleId = db.Roles.Select(r => new SelectListItem()
                {
                    Text = r.RoleName,
                    Value = r.RoleId.ToString()
                });

                ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");

            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }
    }
}