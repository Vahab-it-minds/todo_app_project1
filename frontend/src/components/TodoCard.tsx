import type { Todo } from '../services/todos'
import { useDraggable } from '@dnd-kit/core'

type TodoCardProps = {
  todo: Todo
  onClick: () => void
}

function TodoCard({ todo, onClick }: TodoCardProps) {
    const {
        attributes,
        listeners,
        setNodeRef,
        transform,
        isDragging,
    } = useDraggable({
        id: todo.id,
    })

    const style = transform
        ? {
            transform: `translate3d(${transform.x}px, ${transform.y}px, 0)`,
            opacity: isDragging ? 0.7 : 1,
            zIndex: isDragging ? 100 : 'auto',
        }
        : undefined

  return (
    <article
        ref={setNodeRef}
        style={style}
        {...listeners}
        {...attributes}
        className="todo-card"
        onClick={onClick}
    >
      <h3>{todo.title}</h3>

      {todo.description && (
        <p className="todo-description">{todo.description}</p>
      )}

      <div className="todo-card-footer">
        <span className={`priority priority-${todo.priority.toLowerCase()}`}>
          {todo.priority}
        </span>

        {todo.category && (
          <span className="todo-category">
            {todo.category}
          </span>
        )}
      </div>
    </article>
  )
}

export default TodoCard