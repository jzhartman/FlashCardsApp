using FlashCards.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Application.Interfaces;

public interface ICardRepository
{
    List<Card> GetCardsByStackId();
    void AddCard();
    void DeleteCard();
    void EditCard();
}
