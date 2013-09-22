CREATE TABLE [dbo].[USUARIOS_QUESTIONARIOS] (
    [ID_USUARIO_QUESTIONARIO] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [ID_USUARIO]              NUMERIC (18)  NOT NULL,
    [ID_QUESTIONARIO]         NUMERIC (18)  NOT NULL,
    [CONCLUIDO]               INT           NULL,
    [USUARIO_INCLUSAO]        NVARCHAR (50) NULL,
    [DATA_INCLUSAO]           DATE          NULL,
    [USUARIO_ALTERACAO]       NVARCHAR (50) NULL,
    [DATA_ALTERACAO]          DATE          NULL,
    [EXCLUIDO]                INT           NULL
);





