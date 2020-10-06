begin;

/* create schema smartsaver */
drop schema if exists smartsaver cascade;

create schema smartsaver;

/* create tabe transaction */
-- drop table if exists smartsaver.transaction cascade;

create table smartsaver.transaction (
	id bigserial not null,
	tr_time timestamp,
	amount decimal not null,
	details varchar(1000),
	counter_party varchar(1000),

	constraint "pk_smartsaver.transaction" primary key(id)
);

/* create table goal */
-- drop table if exists smartsaver.goal cascade;

create table smartsaver.goal (
	id bigserial not null,
	goal_date date,
	amount decimal not null,
	title varchar(100),
	description varchar(1000),

	constraint "pk_smartsaver.goal" primary key(id),
	constraint "uq_smartsaver.goal__title_goal_date" unique(title, goal_date),
	CONSTRAINT "uq_smartsaver.goal__amount_positive" CHECK (amount > 0)
);


create table smartsaver.tag (
	id bigserial not null,
	title varchar(100),
	
	constraint "pk_smartsaver.tag" primary key(id)
);

create table smartsaver.transaction_tag (
	-- id bigserial not null,
	transaction_id bigint not null,
	tag_id bigint not null,
	
	-- constraint "pk_smartsaver.transaction_tag" primary key(id),
	constraint "fk_smartsaver.transaction_tag__transaction_id" foreign key(transaction_id)
		references smartsaver.transaction(id),
	constraint "fk_smartsaver.transaction_tag__tag_id" foreign key(tag_id)
		references smartsaver.tag(id)
);

commit;
