CREATE TABLE [dbo].[RESPOSTAS] (
    [ID_RESPOSTA]                    NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [ID_USUARIO]                     NUMERIC (18) NOT NULL,
    [ID_QUESTAO_QUESTIONARIO]        NUMERIC (18) NOT NULL,
    [ID_OPCAO_RESPOSTA_QUESTIONARIO] NUMERIC (18) NOT NULL,
    [DATA_RESPOSTA]                  DATE         NOT NULL,
    [HORA_RESPOSTA]                  TIME (7)     NOT NULL
);









