var PokemonApp = {

    listPageSize: 10,
    Pages: {

        Pokemon: {

            Index: {
                currentQuery: '',
                currentPage: 1,
                load: function () {

                    var loadList = function (pageNumber) {
                        $.ajax({
                            url: "/Pokemon/ListPartial",
                            async: true,
                            accepts: "text/html",
                            type: "GET",
                            data: {
                                pageSize: PokemonApp.listPageSize,
                                page: pageNumber,
                                query: PokemonApp.Pages.Pokemon.Index.currentQuery
                            }
                        })
                        .done(function (result) {
                            $('#pokemon-table').html(result);
                            loadPagination();  
                            loadListActions();
                            PokemonApp.Pages.Pokemon.Index.currentPage = pageNumber;
                        })
                        .fail(function (result) {
                            console.log("Error on load: /Pokemon/ListPartial");
                        });
                    };

                    var loadPagination = function () {
                        $('ul.pagination > li > a').click(function () {

                            loadList(parseInt($(this).html()));

                            return false;
                        });
                    };

                    var loadListActions = function () {

                        // Alteração de status de capturado
                        $('span.btn-toggle-state').click(function () {

                            var pokemonId = $(this).attr('data-item-id');

                            $.ajax({
                                url: "/Pokemon/ToggleCurrentHaveState",
                                async: true,
                                accepts: "text/html",
                                type: "POST",
                                data: {
                                    PokemonId: pokemonId
                                }
                            })
                            .done(function (result) {
                                loadList(PokemonApp.Pages.Pokemon.Index.currentPage);
                                PokemonApp.showModalAlert('Sucesso', 'success', 'Situação alterada com sucesso');
                            })
                            .fail(function (result) {
                                PokemonApp.showModalAlert('Error', 'danger', 'Erro ao alterar situação do Pokémon');
                            });

                        });

                        // Exclusão
                        $('span.btn-delete').click(function () {

                            var pokemonId = $(this).attr('data-item-id');

                            $('#btnConfirmaExclusao').attr('data-item-id', pokemonId);
                            $('#confirm-delete').modal('show');
                        });

                        // Confirmação de exclusão no modal
                        $('#btnConfirmaExclusao').unbind('click').click(function () {

                            var pokemonId = $(this).attr('data-item-id');

                            $.ajax({
                                url: "/Pokemon/Delete",
                                async: true,
                                accepts: "text/html",
                                type: "POST",
                                data: {
                                    PokemonId: pokemonId
                                }
                            })
                            .done(function (result) {
                                loadList(PokemonApp.Pages.Pokemon.Index.currentPage);
                                PokemonApp.showModalAlert('Sucesso', 'success', 'Pokémon excluído com sucesso');
                                $('#confirm-delete').modal('hide');
                            })
                            .fail(function (result) {
                                PokemonApp.showModalAlert('Erro', 'danger', 'Erro ao excluir o Pokémon');
                            });

                        });
                    };

                    var loadBusca = function () {

                        $('button#btnBuscar').click(function () {

                            PokemonApp.Pages.Pokemon.Index.currentQuery = $('input#txtQuery').val();
                            loadList(PokemonApp.Pages.Pokemon.Index.currentPage);

                            return false;
                        });

                    };

                    loadList(PokemonApp.Pages.Pokemon.Index.currentPage);
                    loadBusca();
                }
            },
            Create: {
                load: function () {

                    var getPageData = function () {

                        var name = $('input#txtNome').val();
                        var image = $('input#txtCaminhoImagem').val();
                        var type = $('select#pokemonType').val();

                        return {
                            Name: name,
                            ImagePath: image,
                            CurrentHave: false,
                            PokemonTypeId: parseInt(type)
                        }
                    }

                    var validatePageData = function (pageData) {

                        var result = true;

                        if (pageData.Name.length < 5) {
                            PokemonApp.showModalAlert('Erro', 'danger', 'O nome do Pokémon deve possuir no mínimo 5 caracteres');
                            result = false;
                        } else if (!pageData.ImagePath.startsWith('~/Content/images/')) {
                            PokemonApp.showModalAlert('Erro', 'danger', 'O caminho da imagem não está em um formato válido');
                            result = false;
                        } else if (pageData.PokemonTypeId.length == undefined) {
                            PokemonApp.showModalAlert('Erro', 'danger', 'É obrigatório selecionar um tipo para o Pokémon');
                            result = false;
                        }

                        return result;

                    }

                    var clearPageData = function () {
                        $('input#txtNome').val('');
                        $('input#txtCaminhoImagem').val('');
                        $('select#pokemonType').val('');
                    }

                    clearPageData();

                    $('button#btnSave').click(function () {

                        var pageData = getPageData();
                        if (validatePageData(pageData)) {

                            console.log(pageData);

                            $.ajax({
                                url: "/Pokemon/Create",
                                async: true,
                                accepts: "text/html",
                                type: "POST",
                                data: pageData
                            })
                            .done(function (result) {

                                clearPageData();
                                PokemonApp.showModalAlert('Sucesso', 'success', 'Pokémon cadastrado com sucesso');

                            })
                            .fail(function (result) {
                                PokemonApp.showModalAlert('Erro', 'danger', 'Erro ao cadastrar o Pokémon');
                            });

                        }

                        return false;
                    });

                }
            }

        }

    },

    showModalAlert: function(Title, Class, Content) {
        $('#ModalAlertDiv .modal-title').html(Title);
        $('#ModalAlertDiv .modal-body .callout').attr('class', 'callout no-margin callout-' + Class);
        $('#ModalAlertDiv .modal-body .callout .alert-content').html(Content);
        $('#ModalAlertDiv').modal('show');

        if (Class.indexOf('success') != -1) {
            $('#ModalAlertDiv .modal-body .callout i.fa').attr('class', 'fa fa-check');
            $('#ModalAlertDiv .modal-body .callout div.fa-lg').attr('class', 'text-green fa-lg');
        } else if (Class.indexOf('danger') != -1) {
            $('#ModalAlertDiv .modal-body .callout i.fa').attr('class', 'fa fa-warning');
            $('#ModalAlertDiv .modal-body .callout div.fa-lg').attr('class', 'text-red fa-lg');
        } else if (Class.indexOf('warning') != -1) {
            $('#ModalAlertDiv .modal-body .callout i.fa').attr('class', 'fa fa-warning');
            $('#ModalAlertDiv .modal-body .callout div.fa-lg').attr('class', 'text-yellow fa-lg');
        } else if (Class.indexOf('info') != -1) {
            $('#ModalAlertDiv .modal-body .callout i.fa').attr('class', 'fa fa-info-circle');
            $('#ModalAlertDiv .modal-body .callout div.fa-lg').attr('class', 'text-blue fa-lg');
        }
    }

}