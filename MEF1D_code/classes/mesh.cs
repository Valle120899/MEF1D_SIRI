using System;

namespace Classes
{

    //Se crean cuatro enumeraciones que servirán para dar mayor legibilidad al código
    enum lines { NOLINE, SINGLELINE, DOUBLELINE };
    enum modes { NOMODE, INT_FLOAT, INT_INT_INT };
    enum parameters { ELEMENT_LENGTH, THERMAL_CONDUCTIVITY, HEAT_SOURCE };
    enum sizes { NODES, ELEMENTS, DIRICHLET, NEUMANN };

    //Clase que representa la malla del problema
    class mesh
    {
        float[] parameters = new float[3]; //Para este caso, los valores de l, k y Q
        int[] sizes = new int[4]; //La cantidad de nodos, elementos, condiciones de dirichlet y neumann
        node* node_list; //Arreglo de nodos
        element* element_list; //Arreglo de elementos
        condition* dirichlet_list; //Arreglo de condiciones de Dirichlet
        condition* neumann_list; //Arreglo de condiciones de Neumann
        
            //Método para instanciar el arreglo de parámetros, almacenando los
            //valores de l, k y Q, en ese orden
            public void setParameters(float l, float k, float Q)
        {
            parameters[ELEMENT_LENGTH] = l;
            parameters[THERMAL_CONDUCTIVITY] = k;
            parameters[HEAT_SOURCE] = Q;
        }

        //Método para instanciar el arreglo de cantidades, almacenando la cantidad
        //de nodos, de elementos, y de condiciones (de Dirichlet y de Neumann)
        public void setSizes(int nnodes, int neltos, int ndirich, int nneu)
        {
            sizes[NODES] = nnodes;
            sizes[ELEMENTS] = neltos;
            sizes[DIRICHLET] = ndirich;
            sizes[NEUMANN] = nneu;
        }

        //Método para obtener una cantidad en particular
        public int getSize(int s)
        {
            return sizes[s];
        }

        //Método para obtener un parámetro en particular
        public float getParameter(int p)
        {
            return parameters[p];
        }

        //Método para instanciar los cuatro atributos arreglo, usando
        //las cantidades definidas
        public void createData()
        {
            node_list = new node[sizes[NODES]];
            element_list = new element[sizes[ELEMENTS]];
            dirichlet_list = new condition[sizes[DIRICHLET]];
            neumann_list = new condition[sizes[NEUMANN]];
        }

        //Getters para los atributos arreglo
        public node* getNodes()
        {
            return node_list;
        }
        public element* getElements()
        {
            return element_list;
        }
        public condition* getDirichlet()
        {
            return dirichlet_list;
        }
        public condition* getNeumann()
        {
            return neumann_list;
        }

        //Método para obtener un nodo en particular
        public node getNode(int i)
        {
            return node_list[i];
        }

        //Método para obtener un elemento en particular
        public element getElement(int i)
        {
            return element_list[i];
        }

        //Método para obtener una condición en particular
        //(ya sea de Dirichlet o de Neumann)
        public condition getCondition(int i, int type)
        {
            if (type == DIRICHLET) return dirichlet_list[i];
            else return neumann_list[i];
        }
    };
}