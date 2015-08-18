# CraftingTable.cs System

The CraftingTable.cs System consists of four core classes:
- Ingredients.cs
- Recipe.cs
- RecipeBook.cs
- CraftingTable.cs

With a dependancy on Loot.cs (and Inventory.cs) from https://github.com/trecchia/Loot.cs.git 

## Usage:

### Ingredients.cs

Ingredients uses a Loot object's int ID as a reference and an optional int quantity. Ingredients 
objects are used by passing them into a Recipe object.

#### Example

```c#
// The Loot class requires a string name and an int id
Loot iron = new Loot("Iron", 1);
Loot wood = new Loot("Wood", 2);
Loot sword = new Loot("Sword", 3);

Ingredients ingredients = new Ingredients();
ingredients.Add(1, 2).Add(2);
```

### Recipe.cs

Recipes are used as the basis for the RecipeBook and are referenced when called a method from
the CraftingTable class. They consist of a name, id, RecipeType, Ingredients, and a Loot output.

#### Example

```c#
Recipe swordRecipe = new Recipe("Sword", 1, RecipeType.equipment, ingredients, sword);
```

Optionally:

```c#
Recipe swordRecipe = new Recipe();

swordRecipe.Name = "Sword";
swordRecipe.Id = 1;
swordRecipe.Type = Recipe.RecipeType.equipment;
swordRecipe.Ingredients = ingredients;
swordRecipe.Output = sword;
```

### RecipeBook.cs

RecipeBook is a container class meant to store any Recipes available to the player.

#### Example

```c#
RecipeBook book = new RecipeBook();

book.Add(swordRecipe); // Returns this RecipeBook
book.Total(); // Returns 1
book.Contains(swordRecipe); // Returns true
book.Remove(swordRecipe); // Returns this RecipeBook
```

### CraftingTable.cs

Static class with methods to verify a Recipe can be crafted and to craft a Recipe using
one or more Inventories as a source for the required Ingredients. (Inventory class is part of the 
Loot system found here: https://github.com/trecchia/Loot.cs.git)

#### Example

```c#
Inventory inventoryA = new Inventory();
Inventory inventoryB = new Inventory();

invA.Add(iron, 1);
invA.Add(wood, 1);

invB.Add(iron, 1);

// Returns false as inventoryA does not contain all 
// of the required Ingredients for swordRecipe
bool canCraft = CraftingTable.Craftable(swordRecipe, invA); 

// Returns true as inventoryA and inventoryB combined contain all
// of the required Ingredients for swordRecipe
Inventory[] invList = new Inventory[] {inventoryA, inventoryB};
bool canCraft = CraftingTable.Craftable(swordRecipe, invList);

if (canCraft) {
	Loot craftedSword = CraftingTable.Craft(swordRecipe, invList);
	inventoryA.Add(craftedSword); // inventoryA now contains a crafted Sword Loot object
}
```