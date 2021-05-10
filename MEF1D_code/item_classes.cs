using System;

namespace MEF1D_code
{
    //Se crean cuatro enumeraciones que servirán para dar mayor legibilidad al código
    enum linesE {NOLINE,SINGLELINE,DOUBLELINE};
    enum modesE {NOMODE,INT_FLOAT,INT_INT_INT};
    enum parametersE {ELEMENT_LENGTH,THERMAL_CONDUCTIVITY,HEAT_SOURCE};
    enum sizesE {NODES,ELEMENTS,DIRICHLET,NEUMANN};

    //Clase abstracta que representa un objeto en la malla
    public abstract class Item
    {
        public int id; //identificador
        public float x; //coordenada en X (basta con este dato por estar en 1 dimensión)
        public int node1; //identificador de nodo
        public int node2; //segundo identificador de nodo
        public float value; //valor asociado al objeto

        //Getters para los atributos
        public int getId()
        {
            return id;
        }

        public float getX()
        {
            return x;
        }

        public int getNode1()
        {
            return node1;
        }

        public int getNode2()
        {
            return node2;
        }

        public float getValue()
        {
            return value;
        }

        //Métodos abstractos para instanciar los atributos de acuerdo a las necesidades

        //Caso en que se utiliza un entero y un real
        public abstract void setIntFloat(int n, float r);

        //Caso en que se utilizan tres enteros
        public abstract void setIntIntInt(int n1, int n2, int n3);
    }

    //Clase que representa cada nodo de la malla
    public class node : Item
    {

        //Un nodo usa un entero y un real: su identificador, y su coordenada en X
        public override void setIntFloat(int identifier, float x_coordinate)
        {
            this.id = identifier;
            this.x = x_coordinate;
        }

        public override void setIntIntInt(int n1, int n2, int n3)
        {
        }

    };

    //Clase que representa un elemento en la malla
    public class element : Item
    {

        public override void setIntFloat(int n1, float r)
        {
        }

        //Un elemento usa tres enteros: su identificador, y los identificadores de sus nodos
        public override void setIntIntInt(int identifier, int firstnode, int secondnode)
        {
            id = identifier;
            node1 = firstnode;
            node2 = secondnode;
        }

    };

    //Clase que representa una condición impuesta en un nodo de la malla
    public class condition : Item
    {

        //Una condición usa un entero y un real: un identificador de nodo, y un valor a aplicar
        public override void setIntFloat(int node_to_apply, float prescribed_value)
        {
            node1 = node_to_apply;
            value = prescribed_value;
        }

        public override void setIntIntInt(int n1, int n2, int n3)
        {
        }

    };

    public  class mesh
    {
        float[] parameters = new float[3]; //Para este caso, los valores de l, k y Q
        int[] sizes = new int[4]; //La cantidad de nodos, elementos, condiciones de dirichlet y neumann
       
        protected node[] node_list ;//Arreglo de nodos
        protected element[] element_list; //Arreglo de elementos
        protected condition[] dirichlet_list; //Arreglo de condiciones de Dirichlet
        protected condition[] neumann_list; //Arreglo de condiciones de Neumann


        //Método para instanciar el arreglo de parámetros, almacenando los
        //valores de l, k y Q, en ese orden
        public void setParameters(float l, float k, float Q)
        {
            parameters[(int)parametersE.ELEMENT_LENGTH] = l;
            parameters[(int)parametersE.THERMAL_CONDUCTIVITY] = k;
            parameters[(int)parametersE.HEAT_SOURCE] = Q;
        }

        //Método para instanciar el arreglo de cantidades, almacenando la cantidad
        //de nodos, de elementos, y de condiciones (de Dirichlet y de Neumann)
        public void setSizes(int nnodes, int neltos, int ndirich, int nneu)
        {
            sizes[(int)sizesE.NODES] = nnodes;
            sizes[(int)sizesE.ELEMENTS] = neltos;
            sizes[(int)sizesE.DIRICHLET] = ndirich;
            sizes[(int)sizesE.NEUMANN] = nneu;
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

        //Metodos para inicializar los arreglos
        public void initialize_node(){
            for(int i = 0; i<node_list.Length;i++){
                node_list[i] = new node();
            }
        }
        public void initialize_element(){
            for(int i = 0; i<element_list.Length;i++){
                element_list[i] = new element();
            }
        }
        public void initialize_dirichlet(){
            for(int i = 0; i<dirichlet_list.Length;i++){
                dirichlet_list[i] = new condition();
            }
        }
        public void initialize_neumann(){
            for(int i = 0; i<neumann_list.Length;i++){
                neumann_list[i] = new condition();
            }
        }
        //Método para instanciar los cuatro atributos arreglo, usando
        //las cantidades definidas
        public void createData()
        {
            node_list = new node[sizes[(int)sizesE.NODES]];
            element_list = new element[sizes[(int)sizesE.ELEMENTS]];
            dirichlet_list = new condition[sizes[(int)sizesE.DIRICHLET]];
            neumann_list = new condition[sizes[(int)sizesE.NEUMANN]];

            initialize_node();
            initialize_element();
            initialize_dirichlet();
            initialize_neumann();
        }

        //Getters para los atributos arreglo
        public node[] getNodes()
        {
            return node_list;
        }
        public element[] getElements()
        {
            return element_list;
        }
        public condition[] getDirichlet()
        {
            return dirichlet_list;
        }
        public condition[] getNeumann()
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
            if (type == (int)sizesE.DIRICHLET) return dirichlet_list[i];
            else return neumann_list[i];
        }

        
    };
}