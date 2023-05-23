GO
/****** Object:  StoredProcedure [dbo].[GetClassroomOfHomeroomTeacher]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetClassroomOfHomeroomTeacher]
    @TeacherId int
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT h.* FROM Homerooms h INNER JOIN Teachers t ON h.TeacherId = t.TeacherId
	WHERE h.IsActive = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetHomeroomByTeacherUsername]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetHomeroomByTeacherUsername]
    @TeacherUsername NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT H.*
    FROM [dbo].[Homerooms] AS H
    INNER JOIN [dbo].[Teachers] AS T ON T.[TeacherId] = H.[TeacherId]
    WHERE T.[Username] = @TeacherUsername AND H.[IsActive] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[GetSpecialisationByName]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSpecialisationByName]
    @Name NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [SpecializationId], [NameSpecialization], [IsActive]
    FROM [dbo].[Specializations]
    WHERE [NameSpecialization] = @Name
	AND [IsActive] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[GetSpecializations]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSpecializations]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [SpecializationId], [NameSpecialization], [IsActive]
    FROM [dbo].[Specializations]
    WHERE [IsActive] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentsByUsername]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentsByUsername]
    @Username NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT S.*
    FROM [dbo].[Students] AS S
    WHERE S.[Username] = @Username AND S.[IsActive] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertSpecialization]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertSpecialization]
    @NameSpecialization NVARCHAR(MAX),
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Specializations] ([NameSpecialization], [IsActive])
    VALUES (@NameSpecialization, @IsActive);
END
GO
/****** Object:  StoredProcedure [dbo].[InsertTeacher]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTeacher]
    @Username NVARCHAR(MAX),
    @Name NVARCHAR(MAX),
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Teachers] ([Username], [Name], [IsActive])
    VALUES (@Username, @Name, @IsActive);
END
GO
/****** Object:  StoredProcedure [dbo].[SelectAdminsByUsername]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectAdminsByUsername]
    @Username NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[Admins]
    WHERE [Username] = @Username AND [IsActive] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectTeacherByUsername]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectTeacherByUsername]
    @Username NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM [dbo].[Teachers]
    WHERE [Username] = @Username AND IsActive = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSpecialization]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateSpecialization]
    @SpecializationId INT,
    @NameSpecialization NVARCHAR(MAX),
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Specializations]
    SET [NameSpecialization] = @NameSpecialization,
        [IsActive] = @IsActive
    WHERE [SpecializationId] = @SpecializationId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTeacher]    Script Date: 5/23/2023 5:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateTeacher]
    @TeacherId INT,
    @Username NVARCHAR(MAX),
    @Name NVARCHAR(MAX),
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Teachers]
    SET [Username] = @Username,
        [Name] = @Name,
        [IsActive] = @IsActive
    WHERE [TeacherId] = @TeacherId;
END
GO
