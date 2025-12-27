using FlashCards.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Application.Interfaces;

public interface ICardRepository
{
    Card GetById(int id);
    List<Card> GetAllByStackId(int id);
    int Add(Card card);
    void Delete(int id);
    void Update();
}
