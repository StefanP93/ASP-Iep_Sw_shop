using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IEPProjekatPS120511.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PagedList;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace IEPProjekatPS120511.Controllers
{
    public class OrdersController : Controller
    {
        private IEPProjekatPS120511_dbEntities5 db = new IEPProjekatPS120511_dbEntities5();
        private IEPProjekatPS120511_dbEntities3 dbP = new IEPProjekatPS120511_dbEntities3();
        private IEPProjekatPS120511_dbEntities2 dbU = new IEPProjekatPS120511_dbEntities2();

         public class OrderElement {

             public long OrderId { get; set; }
             public long ProductId { get; set; }
             public string UserId { get; set; }
             public byte[] Image { get; set; }
             public byte[] Logo { get; set; }
             public string ProductName { get; set; }
             public string Tag { get; set; }
             public double ProductPrice { get; set; }
             public string Description { get; set; }
             public DateTime Date { get; set; }
             public string Type { get; set; }
             public string Status { get; set; }

            };

        // GET: Orders
         public ActionResult Index(int? page)
        {

            IEnumerable<OrderElement> ord = new Queue<OrderElement>();

            List<OrderElement> list = new List<OrderElement>();
            
            var order = from m in db.Orders select m;
            
            var userId = User.Identity.GetUserId();

            order = order.Where(s => s.UserId == userId);

            var users = from m in dbU.UserInfoes select m;

            users = users.Where(s => s.UserId == userId);

            ViewBag.UserName = users.First().Firstname;
            ViewBag.LastName = users.First().Lastname;

            foreach (var item in order)
            {
                var productId = item.ProductId;

                var product = dbP.SW_Product.Find(productId);

                OrderElement oE = new OrderElement();

                oE.Logo = product.Logo;
                oE.OrderId = item.OrderId;
                oE.ProductId = product.IDProduct;
                oE.ProductName = product.Name;
                oE.ProductPrice = product.Price;
                oE.Tag = product.Tag;
                oE.UserId = item.UserId;
                oE.Date = item.Date;

                ord=list.AsEnumerable<OrderElement>();

                list.Add(oE);
            }
     
            ViewBag.list = list;

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(ord.ToPagedList(pageNumber, pageSize));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var users = from m in dbU.UserInfoes select m;
            users = users.Where(s => s.UserId == userId);
            ViewBag.UserName = users.First().Firstname;
            ViewBag.LastName = users.First().Lastname;
            
            var productId = order.ProductId;
            var product = dbP.SW_Product.Find(productId);

                       
            OrderElement oE = new OrderElement();

            oE.Logo = product.Logo;
            oE.Image = product.Image;
            oE.Description = product.Description;
            oE.OrderId = order.OrderId;
            oE.ProductId = product.IDProduct;
            oE.ProductName = product.Name;
            oE.ProductPrice = product.Price;
            oE.Tag = product.Tag;
            oE.UserId = order.UserId;
            oE.Date = order.Date;
            
            if (order.Type == 0)
                oE.Type = "New Product";
            else
                oE.Type = "Licence renewal";

            if (order.Status == 0)
                oE.Status = "Waiting for realisation";
            else if (order.Status == 1)
                oE.Status = "Realised";
            else
                oE.Status = "Canceled";
            
            ViewBag.oe = oE;

                return View();
        }

        public void CreatePDF(int? id)
        {
            var order = db.Orders.Find(id);

            var pageSize = new Rectangle(PageSize.A4);
            pageSize.BackgroundColor = new BaseColor(61,61,65);
            Document document = new Document(pageSize);


           // var document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Create a new PdfWrite object, writing the output to a MemoryStream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            // First, create our fonts... (For more on working w/fonts in iTextSharp, see: http://www.mikesdotnetting.com/Article/81/iTextSharp-Working-with-Fonts
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

            titleFont.SetColor(255,255,255);
            bodyFont.SetColor(255, 255, 255);
            subTitleFont.SetColor(255, 255, 255);
            boldTableFont.SetColor(255, 255, 255);
            endingMessageFont.SetColor(255, 255, 255); 

            

            // Add the  title
            document.Add(new Paragraph("IEP sw shop order", titleFont));

            // Now add the "Your order details are below." message
            document.Add(new Paragraph("Thank you for shopping. Your order details are below.", bodyFont));
            document.Add(Chunk.NEWLINE);

            // Add the "Order Information" subtitle
            document.Add(new Paragraph("Order Info", subTitleFont));

            // Create the Order Information table - see http://www.mikesdotnetting.com/Article/86/iTextSharp-Introducing-Tables for more info
            var orderInfoTable = new PdfPTable(2);
            orderInfoTable.HorizontalAlignment = 0;
            orderInfoTable.SpacingBefore = 10;
            orderInfoTable.SpacingAfter = 10;
            orderInfoTable.DefaultCell.Border = 0;

            orderInfoTable.SetWidths(new int[] { 1, 4 });
            orderInfoTable.AddCell(new Phrase("Order ID:", boldTableFont));
            orderInfoTable.AddCell(new Phrase(order.OrderId.ToString() , bodyFont));
            orderInfoTable.AddCell(new Phrase("Date:", boldTableFont));
            orderInfoTable.AddCell(new Phrase(order.Date.ToString(), bodyFont));
            orderInfoTable.AddCell(new Phrase("Status:", boldTableFont));
            if (order.Status == 0)
             orderInfoTable.AddCell(new Phrase("Waiting for realisation", bodyFont));
            else if (order.Status == 1)
                orderInfoTable.AddCell(new Phrase("Realised", bodyFont));
            else 
                orderInfoTable.AddCell(new Phrase("Canceled", bodyFont));
            orderInfoTable.AddCell(new Phrase("Type:", boldTableFont));
           if (order.Type == 0)
                orderInfoTable.AddCell(new Phrase("New Product", bodyFont));
           
            else
                orderInfoTable.AddCell(new Phrase("Licence renewal", bodyFont));
            document.Add(orderInfoTable);

           


            
            document.Add(new Paragraph("Product info:", subTitleFont));
            
            // Create the Order Details table
            var orderDetailsTable = new PdfPTable(2);
            orderDetailsTable.HorizontalAlignment = 0;
            orderDetailsTable.SpacingBefore = 10;
            orderDetailsTable.SpacingAfter = 35;
            orderDetailsTable.DefaultCell.Border = 0;

            var productId = order.ProductId;
            var product = dbP.SW_Product.Find(productId);

            orderDetailsTable.AddCell(new Phrase("Version:", boldTableFont));
            orderDetailsTable.AddCell(new Phrase(product.Tag, bodyFont));
            orderDetailsTable.AddCell(new Phrase("Description:", boldTableFont));
            orderDetailsTable.AddCell(new Phrase(product.Description, bodyFont));
            orderDetailsTable.AddCell(new Phrase("Price:", boldTableFont));
            orderDetailsTable.AddCell(new Phrase(product.Price.ToString(), bodyFont));
            document.Add(orderDetailsTable);

            document.Add(new Paragraph("Buyer info:", subTitleFont));
            
            // Create the Order Details table
            var buyerDetailsTable = new PdfPTable(2);
            buyerDetailsTable.HorizontalAlignment = 0;
            buyerDetailsTable.SpacingBefore = 10;
            buyerDetailsTable.SpacingAfter = 35;
            buyerDetailsTable.DefaultCell.Border = 0;

            var userId = order.UserId;
            var users = from m in dbU.UserInfoes select m;
            users = users.Where(s => s.UserId == userId);

            buyerDetailsTable.AddCell(new Phrase("Name and Surname:", boldTableFont));
            String u = users.First().Firstname + " " + users.First().Lastname;
            buyerDetailsTable.AddCell(new Phrase(u , bodyFont));
           
            document.Add( buyerDetailsTable);



            // Add ending message
            var endingMessage = new Paragraph("Thank you for your purshase!", endingMessageFont);
            document.Add(endingMessage);
            
            
            var logo = iTextSharp.text.Image.GetInstance(product.Logo);
            logo.SetAbsolutePosition(360, 700);
            
            logo.ScaleAbsoluteWidth(200);
            logo.ScaleAbsoluteHeight(50);
            document.Add(logo);

            var prImage = iTextSharp.text.Image.GetInstance(product.Image);
            prImage.SetAbsolutePosition(360, 390);

            prImage.ScaleAbsoluteWidth(200);
            prImage.ScaleAbsoluteHeight(300);
            document.Add(prImage);
            
            document.Close();

     

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition", "attachment; filename= Order.pdf");
            Response.BinaryWrite(output.ToArray());
            response.Flush();
            response.End();

            

            RedirectToAction("Index_RegUser", "SW_Product");
        }

        // GET: Orders/Create
        public ActionResult Create(long? id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductLogo = dbP.SW_Product.Find(id).Logo;
            ViewBag.ProductName = dbP.SW_Product.Find(id).Name;
            ViewBag.ProductPrice = dbP.SW_Product.Find(id).Price;

            var userId = User.Identity.GetUserId();

            var users = from m in dbU.UserInfoes select m;

            users = users.Where(s => s.UserId == userId);

            ViewBag.UserId = userId;
            ViewBag.UserName = users.First().Firstname;
            ViewBag.LastName = users.First().Lastname;

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_O([Bind(Include = "OrderId,UserId,ProductId,Status,Type")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Date = System.DateTime.Now;

                order.Status = 1;

                db.Orders.Add(order);

                db.SaveChanges();

                String payLink = "http://stage.centili.com/widget/WidgetModule?api=00a491cda47c624860e8637e84fd4f85&clientId=" + order.OrderId;

                String payLink2 = "http://stage.centili.com/widget/WidgetModule?api=00a491cda47c624860e8637e84fd4f85";

               // return Redirect(payLink2);

                String callBack = Url.Action("ConfirmSale", "Orders", new { clientId = order.OrderId }, protocol: Request.Url.Scheme);

                Redirect(callBack);

                return RedirectToAction("Index_RegUser", "SW_Product");
            }

            return View(order);
        }


        [AllowAnonymous]
        public  ActionResult ConfirmSale(long clientId)
        {

            var order = db.Orders.Find(clientId);

            order.Status = 1;

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index_RegUser" , "SW_Product");
        }

        public ActionResult CancelSale(long orderId)
        {

            var order = db.Orders.Find(orderId);

            order.Status = 2;

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index_RegUser", "SW_Product");
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserId,ProductId,Status,Type,Date")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
