/**
 * Recipe class helper for CraftingTable.cs and RecipeBook.cs
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

using System;

namespace LootSystem {
    [Serializable]
    public class Recipe {

        private string name;
        private int weight;
        private int id;
        private RecipeType recipeType;
        private Ingredients ingredients;
        private Loot output;

        // Change these to whatever suits your game
        public enum RecipeType { equipment, consumable, upgrade };

        public Recipe() {

        }

        public Recipe(string name, int id, RecipeType recipeType, Ingredients ingredients, Loot output) {
            this.name = name;
            this.id = id;
            this.recipeType = recipeType;
            this.ingredients = ingredients;
            this.output = output;
        }

        public string Name {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Id {
            get { return this.id; }
            set { this.id = value; }
        }

        public RecipeType Type {
            get { return this.recipeType; }
            set { this.recipeType = value; }
        }

        public Ingredients Ingredients {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

        public Loot Output {
            get { return this.output; }
            set { this.output = value; }
        }

        public override bool Equals(object obj) {
            if (GetHashCode() == obj.GetHashCode())
                return true;
            return false;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 47;

                hash = hash * 227 + this.id.GetHashCode();

                return hash;
            }
        }
    }
}
