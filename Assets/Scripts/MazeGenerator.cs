using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    int mazeWidth = 22;
    int mazeDepth = 19;

    int[,] maze = new int[19, 22] {
{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
{ 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
{ 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0 },
{ 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
{ 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0 },
{ 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
{ 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
{ 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
{ 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0 },
{ 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0 },
{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0 },
{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0 },
{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0 },
{ 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0 },
{ 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 0 },
{ 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0 },
{ 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0 },
{ 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0 },
{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }
 };

Positions[,] mazePos = new Positions[0, 0];
    public List<GameObject> tileset = new List<GameObject>();

    Queue<Positions> pathInverted = new Queue<Positions>();
    float tileWidth = 3.0f;
    float tileDepth = 3.0f;

    float xi = -25.0f;
    float zi = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<mazeDepth; i++) //z
        {
            for (int j=0; j<mazeWidth; j++) //x
            {
                GameObject tilePrefab = tileset[maze[i,j]];
                Vector3 p = tilePrefab.transform.position;
                p.x = xi + j * tileWidth;
                p.z = zi - i * tileDepth;

                GameObject newTile = Instantiate(tilePrefab, p, Quaternion.identity) as GameObject;
                
            }
        }

        //maze[0, 1] = 0;
        //maze[18, 20] = 0;

        Vector2Int begin = new Vector2Int(0, 1);
        Vector2Int end = new Vector2Int(18, 20);
        PathFinding(begin, end);

        Queue<Positions> path = new Queue<Positions>();
        Stack<Positions> stack = new Stack<Positions>();
        while (pathInverted.Count != 0)
        {
            stack.Push(pathInverted.Peek());
            pathInverted.Dequeue();
        }
        while (stack.Count != 0)
        {
            path.Enqueue(stack.Pop());
        }

        //Debug de todo o caminho
        //for (int i = 0; i < mazeDepth; i++) //z
        //{
        //    for (int j = 0; j < mazeWidth; j++) //x
        //    {
        //        if (maze[i, j] > 1)
        //        {                   
        //            GameObject tilePrefab = tileset[maze[i, j]];
        //            Vector3 p = tilePrefab.transform.position;
        //            p.x = xi + j * tileWidth;
        //            p.z = zi - i * tileDepth;
        //            GameObject newTile = Instantiate(tilePrefab, p, Quaternion.identity) as GameObject;
        //        }
        //    }
        //}

        while (path.Count != 0)
        {
            Debug.Log(path.Peek().pos);
            GameObject tilePrefab = tileset[maze[path.Peek().pos.x, path.Peek().pos.y]];
            Vector3 p = tilePrefab.transform.position;
            p.x = xi + path.Peek().pos.y * tileWidth;
            p.z = zi - path.Peek().pos.x * tileDepth;
            GameObject newTile = Instantiate(tilePrefab, p, Quaternion.identity) as GameObject;
            path.Dequeue();
        }

    }

    void PathFinding(Vector2Int begin, Vector2Int end)
    {
        Queue<Positions> frontier = new Queue<Positions>();
        Positions current = new Positions();
        current.setPos(begin);
        current.counter = 2;        
        frontier.Enqueue(current);
       
        while (frontier.Count != 0)
        {
            current = frontier.Peek();
            frontier.Peek().setNeighborn(maze, mazeWidth, mazeDepth, frontier.Peek().counter + 1);
            frontier.Peek().alterarMap(frontier, maze, mazeWidth, mazeDepth);
            frontier.Dequeue();
        }
        
        //Caminho Inverso (Encontra o caminho)

        Queue<Positions> ending = new Queue<Positions>();
        
        Positions back = new Positions();
        back.setPos(end);
        back.counter = maze[end.x, end.y];
        ending.Enqueue(back);

        while (ending.Count != 0)
        {
            ending.Peek().findNeighborn(maze, mazeWidth, mazeDepth, ending.Peek().counter);
            if (ending.Peek().hasNeighborns)
            {
                ending.Peek().backingMap(ending, maze, mazeWidth, mazeDepth);
                pathInverted.Enqueue(ending.Peek());
            }            
            ending.Dequeue();
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Positions 
{

    public Vector2Int pos;
    public Vector2Int pointer;
    public Queue<Positions> neighborns = new Queue<Positions>();
    public int counter;
    public bool hasNeighborns;

    public void setPos(Vector2Int position)
    {
        pos = position;
    }
    public void setNeighborn(int[,] maze, int width, int depht, int newCounter)
    {
        Vector2Int neigh = new Vector2Int();
        Positions newNeigh = new Positions();

        neigh.x = pos.x - 1;
        neigh.y = pos.y;
        if (neigh.x >= 0)
            if (maze[neigh.x, neigh.y] == 1)
            {
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = newCounter;
                neighborns.Enqueue(newNeigh);
            }

        neigh.x = pos.x;
        neigh.y = pos.y - 1;
        if (neigh.y >= 0)
            if (maze[neigh.x, neigh.y] == 1)
            {
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = newCounter;
                neighborns.Enqueue(newNeigh);
            }

        neigh.x = pos.x + 1;
        neigh.y = pos.y;
        if (neigh.x < depht)
            if (maze[neigh.x, neigh.y] == 1)
            {                
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = newCounter;
                neighborns.Enqueue(newNeigh);
            }

        neigh.x = pos.x;
        neigh.y = pos.y + 1;
        if (neigh.y < width)
            if (maze[neigh.x, neigh.y] == 1)
            {
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = newCounter;
                neighborns.Enqueue(newNeigh);
            }

    }
    public void findNeighborn(int[,] maze, int width, int depht, int newCounter)
    {
        Vector2Int neigh = new Vector2Int();
        Positions newNeigh = new Positions();

        neigh.x = pos.x - 1;
        neigh.y = pos.y;
        if (neigh.x >= 0)
            if (maze[neigh.x, neigh.y] == counter - 1)
            {
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = counter - 1;
                neighborns.Enqueue(newNeigh);
            }

        neigh.x = pos.x;
        neigh.y = pos.y - 1;
        if (neigh.y >= 0)
            if (maze[neigh.x, neigh.y] == counter - 1)
            {
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = counter - 1;
                neighborns.Enqueue(newNeigh);
            }

        neigh.x = pos.x + 1;
        neigh.y = pos.y;
        if (neigh.x < depht)
            if (maze[neigh.x, neigh.y] == counter - 1)
            {
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = counter - 1;
                neighborns.Enqueue(newNeigh);
            }

        neigh.x = pos.x;
        neigh.y = pos.y + 1;
        if (neigh.y < width)
            if (maze[neigh.x, neigh.y] == counter - 1)
            {                
                newNeigh = new Positions();
                newNeigh.setPos(neigh);
                newNeigh.counter = counter - 1;
                neighborns.Enqueue(newNeigh);
            }

        if (neighborns.Count != 0)
        {
            hasNeighborns = true;
        }
        else
        {
            hasNeighborns = false;
        }

    }

    public int getNeightbornQnt()
    {
        return neighborns.Count;
    }

    public void teste()
    {
        Queue<Positions> teste = new Queue<Positions>();

        while (neighborns.Count != 0)
        {
            Debug.Log("Position: " + counter + " | " + neighborns.Peek().pos);
            teste.Enqueue(neighborns.Peek());
            neighborns.Dequeue();
        }
        neighborns = teste;
        
    }

    public void alterarMap(Queue<Positions> main, int[,] maze, int width, int depht)
    {
        while (neighborns.Count != 0)
        {
            maze[neighborns.Peek().pos.x, neighborns.Peek().pos.y] = counter;
            main.Enqueue(neighborns.Peek());
            neighborns.Dequeue();
        }
    }

    public void backingMap(Queue<Positions> main, int[,] maze, int width, int depht)
    {
        main.Enqueue(neighborns.Peek());
        neighborns.Dequeue();
    }
}

