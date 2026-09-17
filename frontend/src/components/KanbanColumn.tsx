import type { Todo } from '../services/todos'
import TodoCard from './TodoCard'

type KanbanColumnProps = {
  title: string
  todos: Todo[]
  onTodoClick: (todo: Todo) => void
}

function KanbanColumn({ title, todos, onTodoClick, }: KanbanColumnProps) {
  return (
    <section className="kanban-column">
      <div className="kanban-column-header">
        <h2>{title}</h2>
        <span className="task-count">{todos.length}</span>
      </div>

      <div className="kanban-column-content">
        {todos.map((todo) => (
          <TodoCard
            key={todo.id}
            todo={todo}
            onClick={() => onTodoClick(todo)}
          />
        ))}
      </div>
    </section>
  )
}

export default KanbanColumn