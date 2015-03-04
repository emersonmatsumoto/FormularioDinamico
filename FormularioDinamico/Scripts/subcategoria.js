$(function () {
    $("#accordion")
       .accordion({
           header: "> div > h3"
       })
       .sortable({
           axis: "y",
           handle: "h3",
           stop: function (event, ui) {
               // IE doesn't register the blur when sorting
               // so trigger focusout handlers to remove .ui-state-focus
               ui.item.children("h3").triggerHandler("focusout");

               // Refresh accordion to handle new order
               $(this).accordion("refresh");
           },
           stop: function () {
               var inputs = $('input.currentposition');
               $('input.currentposition').each(function (idx) {
                   $(this).val(idx);
               });
           }
       });
    
    var campos = JSON.parse($("#CamposJSON").val());
    $(campos).each(function (i, e) {
        render(e);
    });

    $("#addField").click(function () { addField(); });

    function addField() {
        var campo = { Descricao: "", Tipo: 0, Lista: "", Ordem: $("#accordion").children().size() + 1};
        render(campo);

    }

    function render(campo)
    {
        var index = $("#accordion").children().size();
        var $group = $("<div class='group' />");
        $group.data(campo);
        var $title = $("<h3 />");
        var $titleSpan = $("<span />");
        var $inputs = $("<div />");
        var $inputOrdem = $("<input type='hidden' class='currentposition' id='Campos_" + index + "__Ordem' name='Campos[" + index + "].Ordem' />");

        var $formGroupDesc = $("<div class='form-group' />");
        var $labelDesc = $("<label />");
        var $inputDesc = $("<input class='form-control' id='Campos_" + index + "__Descricao' name='Campos[" + index + "].Descricao' />");
        var $inputError = $("<span class='field-validation-valid text-danger' data-valmsg-for='Campos_" + index + "__Descricao' data-valmsg-replace='true'></span>");

        var $formGroupTipo = $("<div class='form-group' />");
        var $labelTipo = $("<label />");
        var $selectTipo = $("<select class='form-control' id='Campos_" + index + "__Tipo' name='Campos[" + index + "].Tipo' />");
        var $optionCheckbox = $("<option value='0'>checkbox</option>");
        var $optionSelect = $("<option value='1'>select</option>");
        var $optionRadio = $("<option value='2'>radio</option>");
        var $optionText = $("<option value='3'>text</option>");
        var $optionTextarea = $("<option value='4'>textarea</option>");

        var $formGroupLista = $("<div class='form-group' />");
        var $labelLista = $("<label />");
        var $inputLista = $("<input class='form-control' id='Campos_" + index + "__Lista' name='Campos[" + index + "].Lista' />");

        $group.append($title, $inputs);
        $title.append($titleSpan);
        $inputs.append($inputOrdem, $formGroupDesc, $formGroupTipo, $formGroupLista);
        $formGroupDesc.append($labelDesc, $inputDesc, $inputError);
        $formGroupTipo.append($labelTipo, $selectTipo);
        $formGroupLista.append($labelLista, $inputLista);
        $selectTipo.append($optionCheckbox);
        $selectTipo.append($optionSelect);
        $selectTipo.append($optionRadio);
        $selectTipo.append($optionText);
        $selectTipo.append($optionTextarea);

        $titleSpan.text("Campo");
        $labelDesc.append("Descrição");
        $labelTipo.append("Tipo");
        $labelLista.append("Lista");

        $inputOrdem.val(campo.Ordem);
        $inputDesc.val(campo.Descricao);
        $inputDesc.attr("data-val", "true");
        $inputDesc.attr("data-val-length", "Descrição não deve ultrapassar 60 caracteres");
        $inputDesc.attr("data-val-length-max", "60");
        $inputDesc.attr("data-val-required", "Descrição é obrigatório");
        $selectTipo.val(campo.Tipo);
        $inputLista.val(campo.Lista);
        if (campo.Descricao != "")
            $titleSpan.text(campo.Descricao);

        $inputDesc.attr("placeholder", "Campo");
        $inputDesc.change(function () { var v = $(this).val(); $titleSpan.text(v); $group.data().Descricao = v; });

        $inputLista.attr("placeholder", "Separados por vírgula");
        $inputLista.change(function () { var v = $(this).val();  $group.data().Lista = v; });

        $selectTipo.change(function () {
            switch ($(this).val()) {
                case "0": $inputLista.val(""); $formGroupLista.show();
                    break;
                case "1": $inputLista.val(""); $formGroupLista.show();
                    break;
                case "2": $inputLista.val(""); $formGroupLista.show();
                    break;
                case "3": $inputLista.val(""); $formGroupLista.hide();
                    break;
                case "4": $inputLista.val(""); $formGroupLista.hide();
                    break;
                default:
                    break;
            }
        });

        $selectTipo.change(function () { var v = $(this).val(); $group.data().Tipo = v; });

        $("#accordion").append($group);
        $("#accordion").accordion({ active: "#accordion h3:last-child div" }).accordion("refresh");

        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        
    }

   

});