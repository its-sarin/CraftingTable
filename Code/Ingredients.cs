/**
 * Ingredients class helper for CraftingTable.cs, Recipe.cs, and RecipeBook.cs
 * (Requires Loot.cs: https://github.com/trecchia/Loot.cs.git)
 * 
 * Written by Tony Recchia (@then00b) (Copyright © 2015)
 * 
 * 
 * Licensed under the terms of the MIT License
 * ---
 * 
 * Feel free to customize this class to add more fields if desired.
 * 
 *
 * Usage:
 *
 * // Loot class requires a string name and an int id
 * Loot iron = new Loot("Iron", 1);
 * Loot wood = new Loot("Wood", 2);
 * Loot sword = new Loot("Sword", 3);
 *
 * // Ingredients uses a Loot object's int ID to reference it and an optional int quantity
 * Ingredients ingredients = new Ingredients();
 * ingredients.Add(1, 2).Add(2);
 */


using System;
using System.Collections.Generic;

namespace LootSystem {

    [Serializable]
    public class Ingredients {

        private Dictionary<int, int> list;

        public Ingredients() {
            this.list = new Dictionary<int, int>();
        }

        public Ingredients(Dictionary<int, int> ingredientsList) {
            this.list = ingredientsList;
        }

        public Ingredients Add(int lootId, int quantity = 1) {
            this.list.Add(lootId, quantity);

            return this;
        }

        public Ingredients Remove(int lootId, int quantity = 1) {
            if (this.list.ContainsKey(lootId)) {
                if (this.list[lootId] - quantity >= 1) {
                    this.list[lootId] -= quantity;
                } else {
                    this.list.Remove(lootId);
                }
            }

            return this;
        }

        public Dictionary<int, int> Contents {
            get { return this.list; }
        }
    }
}
