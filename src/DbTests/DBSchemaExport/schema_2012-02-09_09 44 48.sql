
    
alter table AuthorsToWrittenBooks  drop constraint FKF7E800A3FBF104B


    
alter table AuthorsToWrittenBooks  drop constraint FKF7E800A1BE8C88E


    
alter table Books  drop constraint FK7416C1F06777A9E4


    drop table Authors

    drop table AuthorsToWrittenBooks

    drop table Books

    drop table Publishers

    drop table Tags

    drop table Users

    create table Authors (
        Id BIGINT IDENTITY NOT NULL,
       AuthorName NVARCHAR(255) null,
       AuthorBirthDate DATETIME null,
       CreatedBy NVARCHAR(255) null,
       CreatedAt DATETIME null,
       LastUpdatedBy NVARCHAR(255) null,
       LastUpdatedAt DATETIME null,
       IsDeleted BIT null,
       primary key (Id)
    )

    create table AuthorsToWrittenBooks (
        Author_id BIGINT not null,
       Book_id BIGINT not null
    )

    create table Books (
        Id BIGINT IDENTITY NOT NULL,
       BookDescription NVARCHAR(2000) null,
       BookName NVARCHAR(255) null,
       PublishedDate DATETIME null,
       CreatedBy NVARCHAR(255) null,
       CreatedAt DATETIME null,
       LastUpdatedBy NVARCHAR(255) null,
       LastUpdatedAt DATETIME null,
       IsDeleted BIT null,
       Publisher_id BIGINT null,
       primary key (Id)
    )

    create table Publishers (
        Id BIGINT IDENTITY NOT NULL,
       PublisherName NVARCHAR(255) null,
       CreatedBy NVARCHAR(255) null,
       CreatedAt DATETIME null,
       LastUpdatedBy NVARCHAR(255) null,
       LastUpdatedAt DATETIME null,
       IsDeleted BIT null,
       primary key (Id)
    )

    create table Tags (
        Id BIGINT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       CreatedBy NVARCHAR(255) null,
       CreatedAt DATETIME null,
       LastUpdatedBy NVARCHAR(255) null,
       LastUpdatedAt DATETIME null,
       IsDeleted BIT null,
       primary key (Id)
    )

    create table Users (
        Id BIGINT IDENTITY NOT NULL,
       Username NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       Email NVARCHAR(255) null,
       CreatedBy NVARCHAR(255) null,
       CreatedAt DATETIME null,
       LastUpdatedBy NVARCHAR(255) null,
       LastUpdatedAt DATETIME null,
       IsDeleted BIT null,
       primary key (Id)
    )

    alter table AuthorsToWrittenBooks 
        add constraint FKF7E800A3FBF104B 
        foreign key (Book_id) 
        references Books

    alter table AuthorsToWrittenBooks 
        add constraint FKF7E800A1BE8C88E 
        foreign key (Author_id) 
        references Authors

    alter table Books 
        add constraint FK7416C1F06777A9E4 
        foreign key (Publisher_id) 
        references Publishers
