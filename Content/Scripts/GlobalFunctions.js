function drawGraphic(CodQuest, CodUser, Action, Div1) {
    $.ajax({
        type: "post",
        url: "Resposta/" + Action,
        traditional: true,
        data: {
            idQuest: CodQuest,
            idUser: CodUser,
            insertResult: false
        },
        success: function (data, text) {
            var list = eval(data);

            if (list != null && list[0] != null) {
                var aux = [['col', 'porcentagem']];

                for (var i = 0; i < list.length; ++i) {
                    aux.push([list[i]["Perfil"], parseFloat(list[i]["ValorRespostas"])]);
                    //aux.push([list[i][1], parseFloat(list[i][2].replace(",", "."))]);
                }

                var table = google.visualization.arrayToDataTable(aux);

                new google.visualization.ColumnChart(document.getElementById(Div1)).
                            draw(table,
                                { width: 900, height: 400,
                                    hAxis: { title: "Perfil" }
                                }
                        );

            }
            else {
                showMessageNoData('#' + Div1);
            }
        }
    });
}