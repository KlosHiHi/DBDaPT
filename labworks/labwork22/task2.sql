--part1
INSERT INTO Import2_Ingredient ([Name])
SELECT DISTINCT value
FROM Import2_PizzaIngredients
	CROSS APPLY string_split(TRIM(LOWER(Ingredients)), ',');

--part2
INSERT INTO Import2_Pizza ([Name], [Weight], Price)
SELECT [Name], Mass, Price
FROM Import2_PizzaIngredients;

--part 3 
INSERT INTO Import2_PizzaIngredient (PizzaId, IngredientId)
SELECT PizzaId, Ingredients
FROM Import2_PizzaIngredients;


