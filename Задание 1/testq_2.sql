SELECT "Products"."Name" AS "Наименование продукции", 
	CONCAT("Sellers"."Surname", "Sellers"."Name") AS "Сотрудник", 
	SUM("Sales"."Quantity") * 100.0 /
		SUM(SUM("Sales"."Quantity"))
			OVER (PARTITION BY "Sales"."IDProd") 
			AS "Процент продаж продукции"
FROM sales."Sales", sales."Products", sales."Sellers"
WHERE "Sales"."IDProd" = "Products"."ID" 
	AND	"Sales"."IDSel" = "Sellers"."ID" 
	AND "Sales"."Date" >= '01.10.2013' AND "Sales"."Date" <= '07.10.2013' 
	AND "Sales"."IDProd" = ANY (SELECT "IDProd" 
							 FROM sales."Arrivals" 
							 WHERE "Arrivals"."Date" >= '07.09.2013' AND "Arrivals"."Date" <= '07.10.2013')
GROUP BY "Sales"."IDProd", "Наименование продукции", "Сотрудник"
ORDER BY "Наименование продукции", "Сотрудник";