CREATE TABLE table_countries (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL UNIQUE,
    continent TEXT NOT NULL,
    area REAL NOT NULL,
    population INTEGER NOT NULL
);

CREATE TABLE table_cities (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    population INTEGER NOT NULL,
    country_id INTEGER NOT NULL,
    is_capital INTEGER NOT NULL,
    FOREIGN KEY(country_id) REFERENCES table_countries(id)
                          ON DELETE NO ACTION 
                          ON UPDATE NO ACTION 
);

INSERT INTO table_countries (name, continent, area, population) VALUES
    ('Россия', 'Европа', 17100000, 146000000),
    ('Китай', 'Азия', 9597000, 1410000000),
    ('США', 'Америка', 9834000, 331000000),
    ('Бразилия', 'Америка', 8516000, 213000000),
    ('Египет', 'Африка', 1001450, 102000000),
    ('Франция', 'Европа', 551695, 67000000),
    ('Индия', 'Азия', 3287263, 1380000000),
    ('Австралия', 'Австралия', 7692024, 25700000);

INSERT INTO table_cities (name, population, country_id, is_capital) VALUES
('Москва', 12500000, (SELECT Id FROM table_countries WHERE Name = 'Россия'), 1),
('Санкт-Петербург', 5380000, (SELECT Id FROM table_countries WHERE Name = 'Россия'), 0),
('Пекин', 21500000, (SELECT Id FROM table_countries WHERE Name = 'Китай'), 1),
('Шанхай', 26300000, (SELECT Id FROM table_countries WHERE Name = 'Китай'), 0),
('Вашингтон', 705000, (SELECT Id FROM table_countries WHERE Name = 'США'), 1),
('Нью-Йорк', 8400000, (SELECT Id FROM table_countries WHERE Name = 'США'), 0),
('Бразилиа', 3010000, (SELECT Id FROM table_countries WHERE Name = 'Бразилия'), 1),
('Каир', 20200000, (SELECT Id FROM table_countries WHERE Name = 'Египет'), 1),
('Александрия', 5200000, (SELECT Id FROM table_countries WHERE Name = 'Египет'), 0),
('Париж', 2140000, (SELECT Id FROM table_countries WHERE Name = 'Франция'), 1),
('Нью-Дели', 29300000, (SELECT Id FROM table_countries WHERE Name = 'Индия'), 1),
('Мумбаи', 20400000, (SELECT Id FROM table_countries WHERE Name = 'Индия'), 0),
('Канберра', 403000, (SELECT Id FROM table_countries WHERE Name = 'Австралия'), 1);