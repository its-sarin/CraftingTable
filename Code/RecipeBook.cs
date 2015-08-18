/**
 * RecipeBook.cs by Tony Recchia (@then00b) (Copyright © 2015)
 * Inventory class for use with Recipe.cs and CraftingTable.cs
 * (Requires Loot.cs: https://github.com/trecchia/Loot.cs.git)
 * 
 * Licensed under the terms of the MIT License
 * ---
 * 
 * RecipeBook.cs is meant to be used in conjunction with Recipe.cs, Loot.cs, and CraftingTable.cs
 *
 * Example:
 * 
 * Usage: 
 * // Loot class requires a string name and an int id
 * Loot iron = new Loot("Iron", 1);
 * Loot wood = new Loot("Wood", 2);
 * Loot sword = new Loot("Sword", 3);
 * 
 * // Ingredients class requires a Loot object and optional int quantity(default 1)
 * Ingredients ingredients = new Ingredients();
 * // Add Iron (ID 1) and Wood (ID 2)
 * ingredients.Add(1, 2).Add(2);
 * 
 * // Recipe class requires a string name, int id, RecipeType, Ingredients, and Loot object output that it produces
 * Recipe swordRecipe = new Recipe("Sword", 1, RecipeType.equipment, ingredients, sword);
 * RecipeBook book = new RecipeBook();
 * book.Add(swordRecipe);
 * 
 */

using System.Collections.Generic;
using System;

namespace LootSystem {

    [Serializable]
    public class RecipeBook {

        private List<Recipe> list;

        public RecipeBook() {
            this.list = new List<Recipe>();
        }

        // [Clear] - Clear the inventory
        public void Clear() {
            this.list.Clear();
        }

        // [Add] - Add a Recipe item to the RecipeBook. Returns this RecipeBook.
        public RecipeBook Add(Recipe recipe) {
            this.list.Add(recipe);

            return this;
        }

        // [Remove] - Remove a Recipe from the RecipeBook
        public RecipeBook Remove(Recipe recipe) {
            if (this.list.Contains(recipe)) {
                this.list.Remove(recipe);
            }

            return this;
        }

        /* [Total] - returns the total number of Recipe in the RecipeBook */
        public int Total() {
            return this.list.Count;
        }

        /* [Contains] - return whether or not a Recipe exists in the RecipeBook */
        public bool Contains(Recipe recipe) {
            return this.list.Contains(recipe);
        }

        public bool Contains(string name) {
            int c = this.list.Count;

            for (int i = 0; i < c; i++) {
                if (list[i].Name == name) return true;
            }

            return false;
        }

        public bool Contains(int id) {
            int c = this.list.Count;

            for (int i = 0; i < c; i++) {
                if (list[i].Id == id) return true;
            }

            return false;
        }

        // [Contents] - get list of Recipes in RecipeBook
        public List<Recipe> Contents {
            get { return this.list; }
        }


    }
}
