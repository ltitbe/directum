SELECT CONCAT("Sellers"."Surname", "Sellers"."Name") AS "Сотрудник", 
		SUM("Sales"."Quantity") AS "Объём продаж"
FROM sales."Sales", sales."Sellers"
WHERE "Sales"."IDSel" = "Sellers"."ID" 
		AND "Sales"."Date" >= '01.10.2013' AND "Sales"."Date" <= '07.10.2013'
GROUP BY "Сотрудник"
ORDER BY "Сотрудник";