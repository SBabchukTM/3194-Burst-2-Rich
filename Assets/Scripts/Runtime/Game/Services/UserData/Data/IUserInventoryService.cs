﻿using System;
using System.Collections.Generic;
using Runtime.Game.ShopSystem;

namespace Runtime.Game.Services.UserData.Data
{
    public interface IUserInventoryService
    {
        event Action<int> BalanceChangedEvent;

        void SetBalance(int balance);
        
        void AddBalance(int amount);

        int GetBalance();

        void UpdateUsedGameItemID(int userGameItemID);

        ShopItem GetUsedGameItem();

        int GetUsedGameItemID();

        void AddPurchasedGameItemID(int userGameItemID);

        List<int> GetPurchasedGameItemsIDs();
    }
}