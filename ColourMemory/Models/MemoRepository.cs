using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColourMemory.Models
{
    public class MemoRepository : IMemoRepository
    {
        string[] cardColors;
        List<Card> cards;
        int points;
        string message;
        public MemoRepository()
        {
            cardColors = new[] { "blue", "green", "red", "cyan", "yellow", "magenta", "black", "grey",
            "blue", "green", "red", "cyan", "yellow", "magenta", "black", "grey"};
            cards = new List<Card>();
            for (int i = 0; i < 16; i++)
            {
                Card card = new Card();
                card.Id = i;
                card.CardColor = cardColors[i];
                cards.Add(card);

            }
            //https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
            cards = cards.OrderBy(i => Guid.NewGuid()).ToList();

            points = 0;
           
        }
        public IEnumerable<Card> GetAllCards()
        {
            return cards;
        }

        public void ChangeCard(Card card)
        {
            var index = cards.IndexOf(card);
            if (index != -1)
                cards[index] = card;
        }

      
        public Card GetCardById(int cardId)
        {
            Card card = cards.FirstOrDefault(c => c.Id == cardId);
            return card;
        }

        public void AddPoint()
        {
            points++;
        }

        public void SubtractPoint()
        {
            points--;
        }

        public int GetPoints()
        {
            return points;
        }

        public void Restart()
        {
            foreach(Card card in cards)
            {
                card.Up = false;
                card.Gone = false;
            }
            //https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
            cards = cards.OrderBy(i => Guid.NewGuid()).ToList();

            points = 0;
            message = null;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
