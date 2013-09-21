    $(document).ready(function () {
        alert(@Questoes);
        $('#btnAvancar').click()(function () {
            alert(@Questoes);
            $('#questao').html(@Questoes);
        });        
    });