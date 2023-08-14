create database cicerone4
use cicerone4
---------------------------------------
create table db_admin
(
admin_id int identity primary key,
admin_username nvarchar(50) not null unique,
admin_password nvarchar(50) not null, 
);
INSERT INTO db_admin
VALUES ('admin','1234'),
('admin2', '2222')

INSERT INTO db_admin
VALUES ('ad','1111')

select * from db_admin



---------------------------------------
create table db_user
(
useer_id int identity primary key,
username nvarchar(50) not null ,
user_pass nvarchar(50) not null,
user_mail nvarchar(50) not null, 
user_contact int not null,
user_nid nvarchar(50) not null,


);
INSERT INTO db_user
VALUES ('aminul','0000','amin@gmail.com',01911111111,'A11111111222')

select * from db_user
----------------------------------------
create table package
(
package_id int identity primary key,
admin_id int not null foreign key references db_admin(admin_id),
package_name nvarchar(50) not null,
package_source nvarchar(50) , 
package_details nvarchar(200),
package_image nvarchar(max)
);


select * from package
-----------------------------------------------
create table tours
(
tour_id int identity primary key,
tour_username nvarchar(50) not null,
tour_usermail nvarchar(50) not null,
tour_date nvarchar(50) not null,
total_destination nvarchar(80) not null, 
tour_details nvarchar(200)
);
select * from tours
----------------------------------------------------
create table booking
(
booking_id int identity primary key,
booking_date nvarchar(50) not null, 
booking_amount nvarchar(50),
ratings nvarchar(50),
booking_cancellation nvarchar(50)
);
-----------------------------------------------------
create table review_rating
(
rr_id int identity primary key,
tour_date nvarchar(50) not null,
reviewer_email nvarchar(80) not null, 
review_star nvarchar(50)  not null, 
review_details nvarchar(50)
);
-------------------------------------------------------
create table blogs
(
blogs_id int identity primary key,
package_id int not null foreign key references package(package_id),
blog_title nvarchar(50) not null,
blog_writer nvarchar(80) not null, 
publish_date nvarchar(50) not null,
blog_details nvarchar(100)  not null, 
blog_ref nvarchar(50)
);

create table payments
(
payment_id int identity primary key,
payable int not null,
due int not null,
trans_id nvarchar (20) not null
);

SELECT * FROM payments