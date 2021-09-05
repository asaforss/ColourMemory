using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColourMemory.Models
{
    public interface IMemoRepository
    {
        void ChangeCard(Card card);
        IEnumerable<Card> GetAllCards();

        Card GetCardById(int cardId);

        void Restart();

        public void AddPoint();

        public void SubtractPoint();

        public int GetPoints();

        public string GetMessage();




    }
}
