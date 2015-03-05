$(function () {
    $.validator.setDefaults({
        ignore: ""
    })

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
        var index = $("#accordion").children().size();
        var campo = { Descricao: "", Tipo: 0, Lista: "", Ordem: index + 1 };
        render(campo);
        $("#accordion").accordion({ active: index });
    }

    function render(campo)
    {
        var index = $("#accordion").children().size();
        var $group = $("<div class='group' />");
        var $title = $("<h3 />");
        var $titleSpan = $("<span />");
        var $inputs = $("<div />");
        var $inputOrdem = $("<input type='hidden' class='currentposition' id='Campos_" + index + "__Ordem' name='Campos[" + index + "].Ordem' />");

        var $formGroupDesc = $("<div class='form-group' />");
        var $labelDesc = $("<label />");
        var $inputDesc = $("<input class='form-control' id='Campos_" + index + "__Descricao' name='Campos[" + index + "].Descricao' />");
        var $inputError = $("<span class='field-validation-valid text-danger' data-valmsg-for='Campos[" + index + "].Descricao' data-valmsg-replace='true'></span>");

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
        var $inputListaError = $("<span class='field-validation-valid text-danger' data-valmsg-for='Campos[" + index + "].Lista' data-valmsg-replace='true'></span>");

        $group.append($title, $inputs);
        $title.append($titleSpan);
        $inputs.append($inputOrdem, $formGroupDesc, $formGroupTipo, $formGroupLista);
        $formGroupDesc.append($labelDesc, $inputDesc, $inputError);
        $formGroupTipo.append($labelTipo, $selectTipo);
        $formGroupLista.append($labelLista, $inputLista, $inputListaError);
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
        $inputDesc.attr("data-val", true);
        $inputDesc.attr("data-val-length", "Descrição não deve ultrapassar 60 caracteres");
        $inputDesc.attr("data-val-length-max", "60");
        $inputDesc.attr("data-val-required", "Descrição é obrigatório");
        $selectTipo.val(campo.Tipo);
        $inputLista.val(campo.Lista);
        $inputLista.attr("data-val", true);
        $inputLista.attr("data-val-required", "Lista é obrigatória");
        if (campo.Descricao != "")
            $titleSpan.text(campo.Descricao);

        $inputDesc.attr("placeholder", "Campo");
        $inputDesc.keyup(function () { var v = $(this).val(); $titleSpan.text(v); });

        $inputLista.attr("placeholder", "Separados por vírgula");

        $selectTipo.change(function () {
            bTipo(parseInt($(this).val()));
        });

        function bTipo(value)
        {
            switch (value) {
                case 0: $inputLista.attr("data-val", true); $formGroupLista.show(); refreshValidator();
                    break;
                case 1: $inputLista.attr("data-val", true); $formGroupLista.show(); refreshValidator();
                    break;
                case 2: $inputLista.attr("data-val", true); $formGroupLista.show(); refreshValidator();
                    break;
                case 3: $inputLista.attr("data-val", false); $formGroupLista.hide(); refreshValidator();
                    break;
                case 4: $inputLista.attr("data-val", false); $formGroupLista.hide(); refreshValidator();
                    break;
                default:
                    break;
            }
        }

        bTipo(campo.Tipo);
        $("#accordion").append($group);
        $("#accordion").accordion({ active: "#accordion h3:last-child div" }).accordion("refresh");

        refreshValidator();
    }

    function refreshValidator() {
        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form");
    }

});