using ColourMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

 

namespace ColourMemory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoRepository _memoRepository;
        
        
       
        public HomeController(ILogger<HomeController> logger, IMemoRepository memoRepository)
        {
            _logger = logger;
            _memoRepository = memoRepository;

        }

        public IActionResult Index()
        {
            IEnumerable<Card> cards=_memoRepository.GetAllCards();
            ViewBag.Points = _memoRepository.GetPoints();
            ViewBag.Timeout = false;
            return View(cards);
        }

        public IActionResult Turn(int id)
        {
            Card card = _memoRepository.GetCardById(id);
            card.Up = true;
            _memoRepository.ChangeCard(card);
            List<Card> cards = _memoRepository.GetAllCards().ToList();
            
            List<Card> upps = cards.Where(c => c.Up == true).ToList();
            if (upps.Count() >1)
            {
                ViewBag.Timeout = true;
            }
            else
            {
                ViewBag.Timeout = false;
            }
            ViewBag.Points = _memoRepository.GetPoints();
            return View("Index", cards);
        }
        public IActionResult TwoTurned()
        { 
            List<Card> upps = _memoRepository.GetAllCards().Where(c => c.Up == true).ToList();
            if (upps.Count() > 1)
            {

                Card card1 = _memoRepository.GetCardById(upps[0].Id);
                Card card2 = _memoRepository.GetCardById(upps[1].Id);
                if (upps[0].CardColor == upps[1].CardColor)
                {

                    card1.Gone = true;
                    card1.Up = false;


                    card2.Gone = true;
                    card2.Up = false;

                    _memoRepository.AddPoint();
                }
                else
                {
                    card1.Up = false;
                    card2.Up = false;
                    _memoRepository.SubtractPoint();
                }
                _memoRepository.ChangeCard(card1);
                _memoRepository.ChangeCard(card2);
            }
            List<Card> cards = _memoRepository.GetAllCards().ToList();
            ViewBag.Timeout = false;
            ViewBag.Points = _memoRepository.GetPoints();
            if (_memoRepository.GetAllCards().Where(c => c.Gone == false).ToList().Count()== 0)
            {
                ViewBag.Message = "The game is over. Do you want to ";
            }
            return View("Index", cards);
        }
        public IActionResult Restart()
        {
            _memoRepository.Restart();
            List<Card> cards = _memoRepository.GetAllCards().ToList();
            ViewBag.Timeout = false;
            ViewBag.Points = _memoRepository.GetPoints();
            return View("Index", cards);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
