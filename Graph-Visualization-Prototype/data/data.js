function createNode(graph)
{
    var node = new Node(0);
    node.data.title = "This is node " + node.id;
    graph.addNode(node);
    drawNode(node);

    var node2 = new Node(1);
    node2.data.title = "This is node " + node2.id;
    graph.addNode(node2);
    drawNode(node2);

    
    var node3 = new Node(2);
    node3.data.title = "This is node " + node3.id;
    graph.addNode(node3);
    drawNode(node3);


    var node4 = new Node(3);
    node4.data.title = "This is node " + node4.id;
    graph.addNode(node4);
    drawNode(node4);


    var node5 = new Node(4);
    node5.data.title = "This is node " + node5.id;
    graph.addNode(node5);
    drawNode(node5);


    var node6 = new Node(5);
    node6.data.title = "This is node " + node6.id;
    graph.addNode(node6);
    drawNode(node6);


    var node7 = new Node(6);
    node7.data.title = "This is node " + node7.id;
    graph.addNode(node7);
    drawNode(node7);


    var node8 = new Node(7);
    node8.data.title = "This is node " + node8.id;
    graph.addNode(node8);
    drawNode(node8);


    var node9 = new Node(8);
    node9.data.title = "This is node " + node9.id;
    graph.addNode(node9);
    drawNode(node9);

    drawEdge(node, node2);
    drawEdge(node, node3);
    drawEdge(node2, node3);

    drawEdge(node3, node4);
    drawEdge(node4, node5);
    drawEdge(node2, node5);
    drawEdge(node3, node5);
    drawEdge(node4, node9);
    drawEdge(node8, node7);
    drawEdge(node6, node8);
    drawEdge(node2, node8);
}