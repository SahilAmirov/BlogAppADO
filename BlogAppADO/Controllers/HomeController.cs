using BlogAppADO.DataAccess;
using BlogAppADO.Models.Dtos;
using BlogAppADO.Models.ModelContainers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static BlogAppADO.Models.Dtos.CommentDtos;
using static BlogAppADO.Models.Dtos.PostDtos;
using static BlogAppADO.Models.EntityModels;

namespace BlogAppADO.Controllers
{
    public class HomeController : Controller
    {
        PostDal postDal = new PostDal();
        CategoryDal categoryDal = new CategoryDal();
        UserDal userDal = new UserDal();
        CommentDal commentDal = new CommentDal();
        
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("Movie", "The Doctor");
            //return Content(HttpContext.Session.GetString("Movie"));
            IndexContainer container = new IndexContainer();
            container.Categories = categoryDal.GetAll();

            foreach (var item in postDal.GetAll())
            {
                InPosts indexPosts = new InPosts();
                indexPosts.ID = item.ID;
                indexPosts.Title = item.Title;
                indexPosts.Slug = item.Slug;
                indexPosts.PhotoLink = item.PhotoLink;
                indexPosts.PublishDate = item.PublishDate;
                indexPosts.UserName = userDal.GetUserWithId(item.UserID).Name;
                indexPosts.CategoryName = categoryDal.GetCategoryWithId(item.CategoryID).Name;

                container.IndexPosts.Add(indexPosts);
            }
            return View(container);
        }

        [HttpGet]
        public IActionResult PostDetails(int id)
        {
            Post post = postDal.GetPostWithId(id);
            User user = userDal.GetUserWithId(post.UserID);
            Category category = categoryDal.GetCategoryWithId(post.CategoryID);
            PostDetailsContainer container = new PostDetailsContainer();

            container.Post.ID = id;
            container.Post.Title = post.Title;
            container.Post.Slug = post.Slug;
            container.Post.Details = post.Details;
            container.Post.PhotoLink = post.PhotoLink;
            container.Post.PublishDate = post.PublishDate;
            container.User.ID = post.UserID;
            container.User.Name = user.Name;
            container.User.Bio = user.Bio;
            container.User.PhotoLink = user.PhotoLink;
            container.Category.ID = category.ID;
            container.Category.Name = category.Name;
            foreach (var item in commentDal.GetComments(id))
            {
                PostDetailComment comment = new PostDetailComment();
                comment.ID = item.ID;
                comment.CommentMsg = item.CommentMsg;
                comment.AddedAt = item.AddedAt;
                comment.UserName = userDal.GetUserWithId(item.UserID).Name;
                comment.UserPhotoLink = userDal.GetUserWithId(item.UserID).PhotoLink;
                container.Comments.Add(comment);
            }
            
            return View(container);
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            userDal.AddUser(user);
            //HttpContext.Response.Cookies.Append("writerID", user.ID.ToString());
            int writerID = userDal.GetUserWithEmail(user.Email).ID;
            HttpContext.Session.SetInt32("writerID", writerID);
            return RedirectToAction("WritersPostsEdit");
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            User user = userDal.Login(Email, Password);
            if (user != null)
            {
                //HttpContext.Response.Cookies.Append("writerID", user.ID.ToString());
                HttpContext.Session.SetInt32("writerID", user.ID);
                return RedirectToAction("WritersPostsEdit");
            }
            ViewBag.LoginError = "Wrong Credentials!";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //HttpContext.Response.Cookies.Delete("writerID");
            HttpContext.Session.Remove("writerID");
            //return Content(HttpContext.Session.GetInt32("writerID"));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult WritePost()
        {
            if (HttpContext.Session.GetInt32("writerID") == null)
            {
                return RedirectToAction("login");
            }

            return View();
        }

        [HttpPost]
        public IActionResult WritePost(AddPost post)
        {
            if (HttpContext.Session.GetInt32("writerID") == null)
            {
                return RedirectToAction("login");
            }
            post.UserID = HttpContext.Session.GetInt32("writerID").Value;
            post.CategoryID = 1;
            postDal.AddPost(post);
            return RedirectToAction("WritersPostsEdit");
        }

        [HttpGet]
        public IActionResult EditPost(int id)
        {
            int writerID = HttpContext.Session.GetInt32("writerID").Value;
            Post post = postDal.GetPostWithId(id);
            if (post.UserID != writerID) { return View("Error"); }
            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(UpdatePost post)
        {
            postDal.UpdatePost(post);
            ViewBag.PostSaved = "Changes Saved";
            return RedirectToAction("WritersPostsEdit");
            //return View(post);
        }

        [HttpGet]
        [Route("home/WritersPosts/{writerID}")]
        public IActionResult WritersPosts(int writerID)
        {
            WritersPostsContainer container = new WritersPostsContainer();

            if (writerID == 0)
                return RedirectToAction("Login");

            List<Post> posts = postDal.GetPostWithUserId(writerID);
            User user = userDal.GetUserWithId(writerID);

            container.WritersPostsUser.ID = writerID;
            container.WritersPostsUser.Name = user.Name;
            container.WritersPostsUser.Bio = user.Bio;
            container.WritersPostsUser.PhotoLink = user.PhotoLink;

            for (int i = 0; i < posts.Count; i++)
            {
                WritersPostsPosts post = new WritersPostsPosts();
                post.ID = posts[i].ID;
                post.Title = posts[i].Title;
                post.Slug = posts[i].Slug;
                post.Details = posts[i].Details;
                post.PhotoLink = posts[i].PhotoLink;
                post.PublishDate = posts[i].PublishDate;
                post.CategoryName = categoryDal.GetCategoryWithId(posts[i].CategoryID).Name;
                post.UserName = user.Name;
                container.WritersPostsPosts.Add(post);
            }

            return View(container);
        }
        
        [HttpGet]
        [Route("home/WritersPostsEdit")]
        public IActionResult WritersPostsEdit()
        {
            WritersPostsEditContainer container = new WritersPostsEditContainer();
            
            int writerID = Convert.ToInt32(HttpContext.Session.GetInt32("writerID"));
            if (writerID == 0)
                return RedirectToAction("Login");

            List<Post> posts = postDal.GetPostWithUserId(writerID);
            User user = userDal.GetUserWithId(writerID);

            container.WritersPostsEditUser.ID = writerID;
            container.WritersPostsEditUser.Name = user.Name;
            container.WritersPostsEditUser.Bio = user.Bio;
            container.WritersPostsEditUser.PhotoLink = user.PhotoLink;

            for (int i = 0; i < posts.Count; i++)
            {
                WritersPostsEditPosts post = new WritersPostsEditPosts();
                post.ID = posts[i].ID;
                post.Title = posts[i].Title;
                post.Slug = posts[i].Slug;
                post.Details = posts[i].Details;
                post.PhotoLink = posts[i].PhotoLink;
                post.PublishDate = posts[i].PublishDate;
                post.CategoryName = categoryDal.GetCategoryWithId(posts[i].CategoryID).Name;
                post.UserName = user.Name;
                container.WritersPostsEditPosts.Add(post);
            }

            return View(container);
        }

        [HttpPost]
        public IActionResult AddComment(AddComment comment)
        {
            if (HttpContext.Session.GetInt32("writerID")==null)
            {
                return RedirectToAction("Login");
                //return Content("Giris yapmaniz gerekiyor");
            }
            Comment comment0 = new Comment();
            comment0.CommentMsg = comment.Message;
            comment0.IsLiked = true;
            comment0.UserID = HttpContext.Session.GetInt32("writerID").Value;
            comment0.PostID = comment.PostID;
            commentDal.AddComment(comment0);
            return RedirectToAction("postdetails", comment.PostID);
        }
    }
}
