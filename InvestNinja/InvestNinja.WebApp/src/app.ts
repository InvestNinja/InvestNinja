import {Indice} from './indice';

export class App {
    heading = "Indice";
    indices: Indice[] = [];
    indiceCodigo = '';
    indiceDescricao = '';

    addIndice() {
        if (this.indiceCodigo) {
            this.indices.push(new Indice(this.indiceCodigo, this.indiceDescricao, 100));
            this.indiceCodigo = '';
            this.indiceDescricao = '';
        }
    }

    removeIndice(indice) {
        let index = this.indices.indexOf(indice);
        if (index !== -1) {
            this.indices.splice(index, 1);
        }
    }
}