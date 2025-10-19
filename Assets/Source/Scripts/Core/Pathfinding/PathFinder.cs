using System.Collections.Generic;

public static class PathFinder
{
    public static List<Node> FindPath(Node start, Node goal, System.Func<Node, bool> blocked)
    {
        if (blocked(goal)) 
            return null;

        var cameFrom = new Dictionary<Node, Node>();
        var queue = new Queue<Node>();
        var visited = new HashSet<Node>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == goal)
                return ReconstructPath(cameFrom, start, goal);

            foreach (var neighborId in current.Neighbors)
            {
                var neighbor = ServiceContainer.Resolve<Board>().GetNode(neighborId);
                if (neighbor == null || visited.Contains(neighbor) || blocked(neighbor))
                    continue;

                visited.Add(neighbor);
                cameFrom[neighbor] = current;
                queue.Enqueue(neighbor);
            }
        }
        return null;
    }

    public static List<Node> ReachableNodes(Node start, System.Func<Node, bool> blocked)
    {
        var result = new List<Node>();
        var visited = new HashSet<Node>();
        var queue = new Queue<Node>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result.Add(current);

            foreach (var neighborId in current.Neighbors)
            {
                var neighbor = ServiceContainer.Resolve<Board>().GetNode(neighborId);
                if (neighbor == null || visited.Contains(neighbor) || blocked(neighbor))
                    continue;

                visited.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }
        return result;
    }

    private static List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node start, Node goal)
    {
        var path = new List<Node> { goal };
        var current = goal;
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}
