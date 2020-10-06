
begin;

truncate smartsaver.transaction restart identity cascade;
truncate smartsaver.goal restart identity cascade;
truncate smartsaver.tag restart identity cascade;
truncate smartsaver.transaction_tag restart identity cascade;

SET CLIENT_ENCODING TO 'UTF8';

insert into smartsaver.transaction
(tr_time, amount, details, counter_party)
values
('2020-05-09', -30.19, '08/05/2020 15:11 kortelė MAXIMA LT', 'MAXIMA LT, X-097\MINDAUGO G. 11\VILNIUS\UNKNOWN      LTU'),
('2020-05-04', -0.5, 'JAUNIMAS paslaugų planas  - mėnesio mokestis už balandžio mėn.', 'SEB bankas'),
('2020-04-27', -9.44, '24/04/2020 13:47  kortelė CITYBEE/VILNIUS', 'CITYBEE               \Ozo gatv  10A                                \VILNIUS      \08200000  GBRGBR'),
('2020-04-27', -5, '24/04/2020 08:02  kortelė CITYBEE/VILNIUS', 'CITYBEE               \Ozo gatv  10A                                \VILNIUS      \08200000  GBRGBR'),
('2020-04-24', 175.5, 'Stipendija', 'Vilniaus universitetas'),
('2020-04-23', -30.85, '22/04/2020 20:50 kortelė VIADA LT ', 'VIADA LT PLK059\KIRTIMU G. 29\VILNIUS\UNKNOWN      LTU'),
('2020-04-23', -1.14, '22/04/2020 20:51 kortelė VIADA LT ', 'VIADA LT PLK059\KIRTIMU G. 29\VILNIUS\UNKNOWN      LTU'),
('2020-04-14', -20, '09/04/2020 21:47  kortelė www.zmonescinema.lt', 'SEB bankas'),
('2020-04-14', 172.27, 'Darbo užmokestis už kovo mėnesį', 'UAB DARBAS');


insert into smartsaver.goal
(goal_date, amount, title, description)
values
('2021-01-01', 300, 'Party','Birthday of the year'),
('2021-02-02', 1000, 'Skiing', 'Skiing in Alps, Italy');


insert into smartsaver.tag
(title)
values
('Groceries'),
('Wage & Salary'),
('Transport'),
('Entertainment');


insert into smartsaver.transaction_tag
(transaction_id, tag_id)
values
(9, 2),
(6, 3),
(7, 3),
(1, 1);

commit; 