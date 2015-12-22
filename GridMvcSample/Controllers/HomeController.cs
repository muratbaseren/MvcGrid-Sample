using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridMvcSample.Controllers
{
    public class HomeController : Controller
    {
        private static List<Kisi> _SampleKisiler = null;

        // GET: Home
        public ActionResult Index()
        {
            if (_SampleKisiler == null)
                _SampleKisiler = Kisi.GetSampleKisiler(20);

            return View(_SampleKisiler);
        }

        public ActionResult Index2()
        {
            if (_SampleKisiler == null)
                _SampleKisiler = Kisi.GetSampleKisiler(20);

            return View(_SampleKisiler);
        }

        public JsonResult GetKisilerAjax()
        {
            //List<string> isimler = new List<string>();

            //isimler.Add("Murat Başeren");
            //isimler.Add("Kadir Başeren");
            //isimler.Add("Şamil Yılmaz");

            //isimler.AddRange(_SampleKisiler.Select(x => x.FullName));

            return Json(_SampleKisiler.Select(x => x.FullName));
        }
    }

    public class Kisi
    {
        [GridMvc.DataAnnotations.GridHiddenColumn()]
        public int Id { get; set; }

        [GridMvc.DataAnnotations.GridColumn(Title="Ad-Soyad", FilterEnabled=true,SortEnabled=true)]
        public string FullName { get; set; }

        [GridMvc.DataAnnotations.GridColumn(Title = "Yaş", SortEnabled = true)]
        public int Age { get; set; }

        [GridMvc.DataAnnotations.GridColumn(Title = "D.Tarihi", FilterEnabled = true, SortEnabled = true, Format="{0:dd.MM.yyyy}")]
        public DateTime Birthdate { get; set; }

        [GridMvc.DataAnnotations.NotMappedColumn()]
        public bool IsActive { get; set; }


        public static List<Kisi> GetSampleKisiler(int count = 10)
        {
            Random rnd = new Random();
            List<Kisi> kisiler = new List<Kisi>();

            for (int i = 0; i < count; i++)
            {
                kisiler.Add(new Kisi()
                {
                    Id = i,
                    FullName = FakeData.NameData.GetFullName(),
                    Age = rnd.Next(10, 99),
                    IsActive = (i % 2 == 0) ? true : false,
                    Birthdate = FakeData.DateTimeData.GetDatetime(
                        DateTime.Now.AddYears(-25), DateTime.Now.AddYears(-2))
                });
            }

            return kisiler;
        }
    }
}