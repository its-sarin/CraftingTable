/**
 * CraftingTable.cs by Tony Recchia (@then00b) (Copyright © 2015)
 * Crafting class for use with Recipe.cs and RecipeBook.cs
 * (Requires Loot.cs and Inventory.cs: https://github.com/trecchia/Loot.cs.git)
 * 
 * Licensed under the terms of the MIT License
 * ---
 * 
 * CraftingTable.cs is meant to be used in conjunction with Recipe.cs, RecipeBook,cs, Loot.cs, and Inventory.cs
 *
 * Example:
 * 
 * Usage: 
 * // Loot class requires a string name and an int id
 * Loot iron = new Loot("Iron", 1);
 * Loot wood = new Loot("Wood", 2);
 * Loot sword = new Loot("Sword", 3);
 * 
 * // Ingredients class requires an int Loot ID and optional int quantity(default 1)
 * Ingredients ingredients = new Ingredients();
 * ingredients.Add(1, 2).Add(2);
 * 
 * // Recipe class requires a string name, int id, RecipeType, Ingredients, and Loot object output that it produces
 * Recipe swordRecipe = new Recipe("Sword", 1, RecipeType.equipment, ingredients, sword);
 * RecipeBook book = new RecipeBook();
 * book.Add(swordRecipe);
 * 
 * Inventory invA = new Inventory();
 * Inventory invB = new Inventory();
 * 
 * invA.Add(wood);
 * invA.Add(iron);
 * invB.Add(iron);
 *
 *
 * // Call Craftable with the Recipe and Inventory that contains the Ingredients to verify that the Loot can be crafted
 * // Here, Craftable will return false because invA does not contain enough Ingredients
 * if (CraftingTable.Craftable(swordRecipe, invA)) {
 *     invA.Add(CraftingTable.Craft(swordRecipe, invA));
 * }
 *
 * // Craftable and Craft can also be called on multiple Inventories
 * Inventory[] invList = new Inventory[] { invA, invB };
 *
 * // Craftable returns true here because it has found the required Ingredients across multiple Inventories
 * if (CraftingTable.Craftable(swordRecipe, invList)) {
 *     // The Craft method returns the crafted Loot item. Here we are adding it directly to an Inventory
 *     invA.Add(CraftingTable.Craft(swordRecipe, invList));
 * }   
 *
 *
 *
 */
using UnityEngine;
using System;
using System.Collections.Generic;

namespace LootSystem {

    [Serializable]
    public static class CraftingTable {

        /* [Craftable] - Returns true or false if Inventory (or Inventories) contains enough materials to craft a given Recipe */
        public static bool Craftable(Recipe recipe, Inventory inventory) {
            // Set ingredient variables
            Dictionary<int, int> ing = recipe.Ingredients.Contents;
            List<int> ingList = new List<int>(ing.Keys);
            int c = ingList.Count;

            // Iterate through all ingredients in the recipe
            for (int i = 0; i < c; i++) {
                // If the inventory contains the current Loot item, continue ...
                if (inventory.Contains(ingList[i])) {
                    // If the inventory's quantity of the current Loot item 
                    // is less then the Recipe's requirements, return false
                    if (inventory.Quantity(ingList[i]) < ing[ingList[i]]) return false;
                }
                // .. otherwise return false 
                else {
                    return false;
                }
            }

            // Return true if all previous checks passed
            return true;
        }

        public static bool Craftable(Recipe recipe, Inventory[] inventories) {
            Dictionary<int, int> ing = recipe.Ingredients.Contents;
            List<int> ingList = new List<int>(ing.Keys);            
            int c = ingList.Count;
            int d = inventories.Length;            
            int lootCount = 0;

            // countList will hold a total count of available Loot for each Recipe Ingredient
            Dictionary<int, int> countList = new Dictionary<int, int>(c);

            // Iterate through all Ingredients in the Recipe
            for (int i = 0; i < c; i++) {
                // Reset lootCount to 0 for this Ingredient
                lootCount = 0;

                // Iterate through all of the Inventories
                for (int j = 0; j < d; j++) {
                    // If the current inventory contains this Ingredient, continue
                    if (inventories[j].Contains(ingList[i])) {
                        // Current Inventory's quantity of the current Recipe Ingredient
                        int q = inventories[j].Quantity(ingList[i]);
                        // Current Recipe Ingredient's quantity requirement
                        int g = ing[ingList[i]];

                        // If the current Inventory contains the Loot item but does not have enough, 
                        // add its quantity to the lootCount
                        if (q < g) {                            
                            lootCount += q;
                        }

                        // If the current Inventory contains the Loot item and has sufficient quantities,
                        // add the required Ingredient quantity to lootCount
                        else if (q >= g) {
                            lootCount += g;
                        } 
                    }                    
                }

                // After iterating through all of the Inventories, set the total accumulated
                // lootCount to a list with the Recipe's index as its index
                countList[i] = lootCount;
            }


            // After iterating through all of our Ingredients and all of our Inventories, 
            // check each value in countList and compare it to its Ingredient's required quantity.
            // If even one value is less than required, we don't have enough Ingredients and return false.
            for (int r = 0; r < c; r++) {
                if (countList[r] < ing[ingList[r]])
                    return false;
            }

            // If the previous checks passed, return true
            return true;
        }

        // [Craft]
        public static Loot Craft(Recipe recipe, Inventory inventory) {
            // Set ingredient variables
            Dictionary<int, int> ing = recipe.Ingredients.Contents;
            List<int> ingList = new List<int>(ing.Keys);
            int c = ingList.Count;

            // For each Ingredient in the Recipe, remove the required amount from this Inventory
            for (int i = 0; i < c; i++) {
                inventory.Remove(ingList[i], ing[ingList[i]]);
            }

            return recipe.Output;
        }

        public static Loot Craft(Recipe recipe, Inventory[] inventories) {
            Dictionary<int, int> ing = recipe.Ingredients.Contents;
            List<int> ingList = new List<int>(ing.Keys);
            int c = ingList.Count;

            for (int i = 0; i < c; i++) {
                // For each Ingredient in the Recipe, set the required amount and reset the number removed
                int req = ing[ingList[i]];
                int numRemoved = 0;

                for (int j = 0; j < inventories.Length; j++) {
                    int q = inventories[j].Quantity(ingList[i]);
                    Debug.Log(q);

                    // If the current Inventory contains all of the required Ingredients,
                    // remove them all and break out of this loop.
                    if (q >= ing[ingList[i]]) {
                        inventories[j].Remove(ingList[i], req);
                        numRemoved = req;
                        break;
                    }
                    // If the current Inventory contains only some of the required Ingredients,
                    // remove however many it contains, increment numRemoved, and check numRemoved
                    // to see if we've removed the required amount. If so, break out of this loop.
                    else {
                        inventories[j].Remove(ingList[i], q);
                        numRemoved += q;

                        if (numRemoved >= req)
                            break;
                    }
                }
            }

            return recipe.Output;
        }

    }
}
