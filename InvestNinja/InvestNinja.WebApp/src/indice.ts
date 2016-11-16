export class Indice {
    done = false;
    descricao: string;
    codigo: string;
    valorCotaInicial: number;

    constructor(codigo, descricao: string, valorCotaInicial: number) {
        this.codigo = codigo;
        this.descricao = descricao;
        this.valorCotaInicial = valorCotaInicial;
    }
}