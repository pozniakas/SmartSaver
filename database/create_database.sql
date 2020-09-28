begin;

/* Create schema smartsaver */
drop schema if exists smartsaver cascade;

create schema smartsaver;

/* Create tabe transaction */
drop table if exists smartsaver.transaction cascade;

create table smartsaver.transaction (
	id bigserial not null,
	tr_time timestamp,
	amount decimal not null,
	details varchar(1000),
	counter_party varchar(1000),

	constraint "pk_smartsaver.transaction" primary key(id)
);

/* Create table goal */
drop table if exists smartsaver.goal cascade;

create table smartsaver.goal (
	id bigserial not null,
	goal_date date,
	amount decimal not null,
	title varchar(100),
	description varchar(1000),

	constraint "pk_smartsaver.goal" primary key(id),
	CONSTRAINT "uq_smartsaver.goal__title_goal_date" UNIQUE(title, goal_date)
);

commit;
